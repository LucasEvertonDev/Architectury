# Importante
Todo o contexto do projeto foi feito pra estudo. Visando muito mais arquitetura do que qualidade do software escrito(Famoso casa de ferreiro espeto é de pau)

# Validações dos objeto das requisições
Neste modelo foi usado o fluent validation. O mesmo está sendo ativado de forma assincrona e automática por meio de um filter. Se faz necessário pois o pipeline do .net core não é assincrono. Sendo assim seria impossível fazer verificações junto ao banco. 

# Camada Application
Foi usado o conceito de casos de uso onde cada ação contemsua própria classe. Ao contrário do modelo de service que agrupa ações por cenário em uma única classe.

# Camada Domain
Foi usada um lib que ainda estou implementando. O objetivo dela é aplicar as validações de forma simples e prática. Sendo que a mesma alimenta o padrão notification pattern.
A lib tem como objetivo ajudar a aplicar as regras de negócio buscando manter o modelo no sempre válido. Garantindo uma melhor qualidade de software e fácil vislumbramento de boa parte das regras sobre o dominio.

# Repositorios
Nessa versão está sendo usada uma classe unica para o contexto de repository da entidade. Foi Adotado o modelo de unit of work que tem como vantagens deixar os use cases mais limpos.

# Notification Pattern 
Devido melhora na performance, foi escolhido o modelo de notification. Tem como objetivo reservar as exceptions apenas para casos inesperados. Resultando em um aumento de performance.

# Controllers
Está sendo usado. Porém ele está tendo uma ação básica. Nesse momento ele ainda trata por mais que de forma repetida as notificações. Embora eu possa criar um filtro, e tratar em background isso da uma sensação de fantasma na aplicação.
Onde eu simplesmente irei retornar nada em um momento abstrato e acabará de maneira oculta retornando um json. Um pouco confuso além de deixar o processo meio macabro.

# Testes
A aplicação foi fundade em cima de uma classe espelho do appsettings. Tal abordagem facilita totalmente a inserção de testes unitários. Uma vez que setadan os testes vivem o mesmo cenário de uma requisição comum. O que facilita testes unitários a níveis de banco.
Tendo por objetivo não ter que criar repositorios fakes para tal fato. 
Para garantir uma ação de crud foi implementada a ordem de prioridade para que os testes possam ser executados em etapas. Considerando que estejam em uma classe. Ou seja um item inserido no momento de teste poderá ser consultado, atualizado, apagado. Deixando todo o seu ciclo de vida testável
