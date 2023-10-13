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
Nesse modelo foi usado um repositorio único que possui abstrações segmentadas de acordo com ações(Insert, delete, update, Search). Por mais que apresenta um boa prática é mesmo é bastante trabalhosa. A qual degve ser análisada se realmente é valida para o cenário.

# Notification Pattern 
Devido melhora na performance, foi escolhido o modelo de notification. Tem como objetivo reservar as exceptions apenas para casos inesperados. Resultando em um aumento de performance.

# Controllers
Está sendo usado. Porém ele está tendo uma ação básica. Nesse momento ele ainda trata por mais que de forma repetida as notificações. Embora eu possa criar um filtro, e tratar em background isso da uma sensação de fantasma na aplicação.
Onde eu simplesmente irei retornar nada em um momento abstrato e acabará de maneira oculta retornando um json. Um pouco confuso além de deixar o processo meio macabro.
