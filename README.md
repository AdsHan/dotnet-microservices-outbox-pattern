# core-microservices-outbox-pattern
Demonstração de mensageria utilizada em conjunto com Outbox Pattern para garantir a atomicidade com o armazenamento dos Integration Events.

**O problema:** Após realizar uma transação (commit) que efetua a persistência dos dados, pode ser necessária a publicação de uma mensagem no broker! Ocorre que o commit e a publicação da mensagem são operações distintas, não-atômicas. Logo, após o commit, o broker pode ficar indisponível e a mensagem não ser publicada. Para isso existe o Pattern Outbox, onde os dados da mensagem são armazenados como uma transação junto com a persistência da entidade/agregado. 
 
# Este projeto contém:
- Arquitetura Microsserviços;
- RabbitMQ como messaging broker;
- Message Bus;
- Persistência em SQLServer da entidade/agregado;
- Persistência em MongoDB dos Integration Events;
- Pattern CQRS com MediatR;
- Pattern Repository;
- Fluent Validation;
- Mapeamento das entidades por Fluent API;
- Entity Framework (EF) Core; 

# Como executar:
- Clonar / baixar o repositório em seu workplace.
- Baixar o .Net Core SDK e o Visual Studio / Code mais recentes.
- Instalar o RabbitMQ local ou em container.
- Instalar o MongoDB local ou em container.

# Sobre
Este projeto foi desenvolvido por Anderson Hansen sob [MIT license](LICENSE).
