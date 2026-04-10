// Representa as configuracoes globais do sistema de notificacoes.
// Esta classe usa o padrao Singleton para garantir uma unica instancia compartilhada.
class Settings
{
    private static Settings instance;

    private string Name = "Name";
    private string ChannelType  = "email";
    private int MaxRetries = 3;

    private Settings() {}

    public static Settings GetInstance() {
        if (instance == null) {
            instance = new Settings();
        }
        return instance;
    }

    public void SetName(string name) {
        this.Name = name;
    }

    public void SetChannelType(string channelType) {
        this.ChannelType = channelType;
    }

    public void SetMaxRetries(int maxRetries) {
        this.MaxRetries = maxRetries;
    }

    public string GetName() {
        return this.Name;
    }

    public string GetChannelType() {
        return this.ChannelType;
    }

    public int GetMaxRetries() {
        return this.MaxRetries;
    }
}


// Fabrica responsavel por criar o canal de notificacao adequado com base no tipo informado.
static class ChannelFactory {
    // Seleciona e instancia o canal correspondente ao valor recebido.
    public static INotificationChannel CreateChannel(string channelType) {
        INotificationChannel channel;
        switch (channelType) {
            case "email":
                channel =  new EmailNotificationChannel();
                break;
            case "sms":
                channel = new SmsNotificationChannel();
                break;
            case "push":
                channel = new PushNotificationChannel();
                break;
            default:
                throw new ArgumentException("Invalid channel type");
        }
        return new NotificationProxy(channel);
    }
}

// Define o contrato comum para qualquer canal de notificacao.
interface INotificationChannel
{
    void Send(string message);
}

class NotificationProxy : INotificationChannel
{
    private INotificationChannel realChannel;
    private Settings settings;

    public NotificationProxy(INotificationChannel channel) {
        this.realChannel = channel;
        this.settings = Settings.GetInstance();
    }

    public void Send(string message)  {
        int attempts = 0;
        int maxRetries = settings.GetMaxRetries();

        while (attempts < maxRetries)  {
            try 
            {
                Console.WriteLine($"[Proxy] Attempt {attempts + 1}");
                this.realChannel.Send(message);
                return;
            }
            catch (Exception ex) 
            {
                attempts++;
                Console.WriteLine($"[Proxy] Error: {ex.Message} Retrying...");
            }
        }

        Console.WriteLine("[Proxy] All attempts failed.");
    }
}

// Implementa o envio de notificacoes por email.
class EmailNotificationChannel : INotificationChannel
{
    public void Send(string message)
    {
        Console.WriteLine($"Sending email: {message}");
    }
}

// Implementa o envio de notificacoes por SMS.
class SmsNotificationChannel : INotificationChannel
{
    public void Send(string message)
    {
        Console.WriteLine($"Sending SMS: {message}");
    }
}

// Implementa o envio de notificacoes por push.
class PushNotificationChannel : INotificationChannel
{
    public void Send(string message)
    {
        Console.WriteLine($"Sending push notification: {message}");
    }
}

// Classe que não segue o padrão da interface
class AlternativeNotificationChannel
{
    public void AlternativeSend(string message)
    {
        Console.WriteLine($"Sending alternative notification: {message}");
    }
}

// Adaptador para a classe inconsistente
class AlternativeChannelAdapter : INotificationChannel
{
    private AlternativeNotificationChannel alternativeChannel;

    public AlternativeChannelAdapter(AlternativeNotificationChannel alternativeChannel)  {
        this.alternativeChannel = alternativeChannel;
    }
    
    public void Send(string message) {
        this.alternativeChannel.AlternativeSend(message);
    }
}


// Ponto de entrada da aplicacao.
// O objetivo principal aqui e demonstrar como obter as configuracoes e criar
// um canal de notificacao a partir da fabrica para enviar uma mensagem.
class Program
{
    static void Main(string[] args)
    {
        // Obtem a instancia unica de configuracao e define os parametros.
        Settings settings = Settings.GetInstance();
        settings.SetName("MyNotificationSystem");
        settings.SetChannelType("email");
        settings.SetMaxRetries(3);

        // Cria o canal de notificacao usando a fabrica com base na configuracao.
        INotificationChannel channel = ChannelFactory.CreateChannel(settings.GetChannelType());

        // Envia uma mensagem de teste usando o canal criado.
        channel.Send("Hello, this is a test notification!");
    }
}