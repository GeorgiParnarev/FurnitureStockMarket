# Онлайн магазин за мебели

## Цел и предназначение
Онлайн магазинът за мебели има за цел да предостави на клиентите лесен и удобен начин за избор и покупка на качествени мебели за дома и офиса.

## Описание на приложението
Приложението е изградено като MVC приложение с три слоя:

### FurnitureStockMarket.Database
В този слой се намира модела, описващ базата данни.

![image](https://github.com/GeorgiParnarev/FurnitureStockMarket/assets/131038567/eb943e13-3625-44fd-b2b4-dce0ff3c4909)

#### Модели:
- `Category`: Модел на категориите на различните мебели/продукти.
- `Customer`: Модел на клиента с нелични данни.
- `Order`: Модел за поръчките на клиента.
- `Product`: Модел за данните на даден мебел/продукт.
- `ProductsOrders`: Модел, който свързва поръчките с продуктите.
- `Review`: Модел за ревюта на даден продукт.
- `SubCategory`: Модел на съб-категориите на различните мебели/продукти.
- Допълнителни модели:
  - `ApplicationUser`: Наследяващ `IdentityUser` модел, добавящ допълнителни данни за потребител.
  - `ApplicationRole`: Наследяващ `IdentityRole` модел, съхраняващ данни за ролите.
  
В слоя се намира и интерфейсът `IRepository`, деклариращ абстрактни методи за достъп до хранилището на данни - Microsoft SQL Server, както и класа `Repository`, имплементиращ `IRepository`, внедряващ методите за достъп до хранилището на релационната база данни. Класът `Repository` и интерфейсът `IRepository` са разработени от Стамо Петков и са предоставени от него за ползване от студентите на SoftUni за разработката на учебни WEB проекти.

### FurnitureStockMarket.Core (слой с бизнес логика)
В този слой са вградени сървиси и модели с чиято помощ се осъществява обработката на получените от потребителите данни и препращането им към базата данни.

#### Сървиси:
- `AccountService`: Манипулира данните при работа с акаунти на потребители, включително регистрация, логин и излизане.
- `AdminService`: Сървис за действията, които администраторът може да извършва, включително добавяне и редактиране на мебели, категории и съб-категории, както и преглед на поръчките на клиентите.
- `HomeService`: Сървис за всички мебели/продукти в магазина, сортирани по рейтинг.
- `MenuSearchService`: Сървис за сърч бара и менюто с различните категории на продуктите.
- `OrderService`: Сървис, отговарящ за създаването и отказа на поръчки.
- `ReviewService`: Сървис за добавяне на ревюта на продуктите.
- `ShoppingCartService`: Сървис за управление на количката с продукти на клиента, включително добавяне, намаляване на бройката и премахване на продуктите.

### FurnitureStockMarket (WEB слой)
В този слой се намират контролерите, визуалните модели и изгледите, позволяващи комуникацията между сървъра и клиентите (потребителите на приложението).

#### Контролери:
- `AccountController`: Управлява визуализацията и данните, свързани с акаунтите на потребителите.
- `HomeController`: Управлява визуализацията на мебелите/продуктите и техните детайли.
- `MenuSearchController`: Управлява визуализацията на менюто с категориите и получаването на данни от сърч бара.
- `OrderController`: Управлява визуализацията на поръчките, които клиентите са направили.
- `ReviewController`: Управлява визуализацията и създаването на ревюта за даден мебел/продукт.
- `ShoppingCartController`: Управлява визуализацията на количката за продукти.

#### Admin area:
- `AdminController`: Управлява визуализацията и получаването на данни, свързани с добавянето на нови продукти, категории, съб-категории и визуализацията на поръчките на клиентите. Достъпът е ограничен само за администратори.

## Тестови данни
- Администратор: Потребителско име - `GogoGogov20`, Имейл - `GogoGogov@abv.bg`, Парола - `123456`
- Потребител: Потребителско име - `MishoMishov`, Имейл - `MishoMishov@abv.bg`, Парола - `123456`
