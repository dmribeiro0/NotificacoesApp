// Representa as configuracoes globais do sistema de notificacoes.
// Esta classe usa o padrao Singleton para garantir uma unica instancia compartilhada.
class Settings
{
    private static Settings instance;

    private string Name { get; set; } = "Name";
    private string ChannelType { get; set; } = "email";
    private int MaxRetries { get; set; } = 3;

    private Settings() {}

    public static Settings getInstance() {
        if (instance == null) {
            instance = new Settings();
        }
        return instance;
    }
}


// Fabrica responsavel por criar o canal de notificacao adequado com base no tipo informado.
static class ChannelFactory {
    // Seleciona e instancia o canal correspondente ao valor recebido.
    public static NotificationChannel CreateChannel(string channelType) {
        switch (channelType) {
            case "email":
                return new EmailNotificationChannel();
            case "sms":
                return new SmsNotificationChannel();
            case "push":
                return new PushNotificationChannel();
            default:
                throw new ArgumentException("Invalid channel type");
        }
    }
}

// Define o contrato comum para qualquer canal de notificacao.
interface NotificationChannel
{
    void Send(string message);
}

// Implementa o envio de notificacoes por email.
class EmailNotificationChannel : NotificationChannel
{
    public void Send(string message)
    {
        Console.WriteLine($"Sending email: {message}");
    }
}

// Implementa o envio de notificacoes por SMS.
class SmsNotificationChannel : NotificationChannel
{
    public void Send(string message)
    {
        Console.WriteLine($"Sending SMS: {message}");
    }
}

// Implementa o envio de notificacoes por push.
class PushNotificationChannel : NotificationChannel
{
    public void Send(string message)
    {
        Console.WriteLine($"Sending push notification: {message}");
    }
}

// Ponto de entrada da aplicacao.
// O objetivo principal aqui e demonstrar como obter as configuracoes e criar
// um canal de notificacao a partir da fabrica para enviar uma mensagem.
class Program
{
    static void Main(string[] args)
    {
        var settings = Settings.getInstance();
        var channel = ChannelFactory.CreateChannel("email");
        channel.Send("Hello, World!");
    }
}