Este projeto implementa um sistema simples de simulação de envio de notificações utilizando quatro padrões de projeto: Factory, Singleton, Proxy e Adapter.

O código pode ser acessado pelo arquivo Program.cs.

O padrão Factory é utilizado na classe ChannelFactory, que centraliza a criação dos diferentes tipos de canais de notificação (email, SMS e push). Isso permite que o sistema seja facilmente extensível, possibilitando a adição de novos canais sem a necessidade de modificar o código existente que utiliza a fábrica.

O padrão Singleton é aplicado na classe Settings, garantindo que exista apenas uma única instância responsável por armazenar as configurações globais do sistema, como nome da aplicação, tipo de canal e número máximo de tentativas de envio.

A utilização desses padrões contribui para a redução de acoplamento, melhor organização do código e maior facilidade de manutenção e evolução do sistema.

O padrão Proxy está aplicado na classe NotificationProxy que adiciona uma nova funcionalidade de retry ao processo de envio de notificações, delegando a chamada para o canal real. Isso permite que o sistema seja mais robusto, lidando melhor com falhas temporárias no envio.

O padrão Adapter é utilizado na classe AlternativeChannelAdapter, que adapta a interface de um canal de notificação específico (AlternativeNotificationChannel) para a interface genérica INotificationChannel, permitindo que o sistema utilize diferentes tipos de canais de forma transparente.

A solução está de acordo com os conceitos apresentados no Capítulo 6 do livro Engenharia de Software Moderna, de Marco Túlio Valente, que aborda o uso de padrões de projeto para melhorar a qualidade e a flexibilidade de sistemas orientados a objetos.