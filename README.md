Este projeto implementa um sistema simples de envio de notificações utilizando dois padrões de projeto: Factory e Singleton.

O código pode ser acessado pelo arquivo Program.cs.

O padrão Factory é utilizado na classe ChannelFactory, que centraliza a criação dos diferentes tipos de canais de notificação (email, SMS e push). Isso permite que o sistema seja facilmente extensível, possibilitando a adição de novos canais sem a necessidade de modificar o código existente que utiliza a fábrica.

O padrão Singleton é aplicado na classe Settings, garantindo que exista apenas uma única instância responsável por armazenar as configurações globais do sistema, como nome da aplicação, tipo de canal e número máximo de tentativas de envio.

A utilização desses padrões contribui para a redução de acoplamento, melhor organização do código e maior facilidade de manutenção e evolução do sistema.

A solução está de acordo com os conceitos apresentados no Capítulo 6 do livro Engenharia de Software Moderna, de Marco Túlio Valente, que aborda o uso de padrões de projeto para melhorar a qualidade e a flexibilidade de sistemas orientados a objetos.