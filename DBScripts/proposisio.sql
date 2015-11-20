use [DiamondDB]
insert into AccountPrivileges (Id,AccountType)
values (1,'a'),(2,'s');

insert into Users (Name,Surname,PhoneNum, Email,Position,AccountType,Login,Password,FirstLogin)
values 
('Lidia' , 'Torzewska',	'512971553',	'ltorzewska@hotelediament.pl'	,'Regionalny Kierownik Sprzedaży',2, 'lito','test','t'),
('Agata', 'Kleszko',	'506102076',	'akleszko@hotelediament.pl','Specjalista ds. sprzedaży',2,'agkl','test','t'),
('Daria', 'Drewniok',	'506100952',	'ddrewniok@hotelediament.pl',	'Specjalista ds. sprzedaży',2,'dadr','test','t'),
('Paweł','Jaszczyk',	'512007210','pjaszczyk@hotelediament.pl',	'Specjalista ds. sprzedaży',2 , 'paja','test','t'),
( 'Michał',' Witkowicz',	'609060268',	'mwitkowicz@hotelediament.pl',	'Specjalista ds. sprzedaży',1,'miwi','test','t'),
('Tomasz', 'Antolak',	'603051705',	'tantolak@hotelediament.pl',	'Specjalista ds. sprzedaży',2,'toan','test','t'),
('Małgorzata', 'Michalak'	,'695219621',	'mmichalak@hotelediament.pl',	'Contracting Manager',2,'mami','test','t'),
('Tomasz', 'Sikora',	'512971529',	'tsikora@hotelediament.pl',	'Specjalista ds. Sprzedaży',2,'tosi','test','t'),
('Anna', 'Rosińska',	NULl,	'arosinska@hotelediament.pl',	'Specjalista ds. Sprzedaży',2,'anro','test','t'),
('Maria' ,'Bonk',	'506101535',	'mbonk@hotelediament.pl',	'Specjalista ds. Sprzedaży',2,'mabo','test','t'),
('Sylwia', 'Śliwka', '506101958',	'ssliwka@hotelediament.pl',	'Specjalista ds. Klienta wypoczynkowego',2,'sysl','test','t'),
('Joanna', 'Gwiazdowska',	'605882838',	'jgwiazdowska@hotelediament.pl','Specjalista ds. Sprzedaży MICE',2,'jogw','test','t'),
('Monika', 'Medwid',	'512971368',	'mmedwid@hotelediament.pl',	'Specjalista ds. Sprzedaży',2,'mome','test','t' ),
('Marcin', 'Budniok', 	'667671471', 	'marcin.budniok@stylehotels.pl',	'Kierownik Działu Sprzedaży',2,'mabu','test','t'),
('Daria', 'Drewniok',	'506100952',	'ddrewniok@hotelediament.pl',	'Specjalista ds. sprzedaży',2,'dadr','test','t'),
('Justyna', 'Majchrzak',	'512971568','jmajchrzak@hotelediament.pl',	'Specjalista ds. sprzedaży',2,'juma','test','t'),
('Magdalena', 'Jędruszek', 	'723550657',	'mjedruszek@hotelediament.pl',	'Specjalista ds. sprzedaży',2,'maje','test','t'),
('Magdalena', 'Radzikowska-Buchała',	'506101894',	'mradzikowska@hotelediament.pl',	'Specjalista ds. Sprzedaży',2,'mara','test' ,'t'),
('Magdalena', 'Szopa',	'512971427',	'mszopa@hotelediament.pl',	'Specjalista ds. Sprzedaży',2,'ma','sz' ,'t')

insert into Proposition (Id_user, UpdateDate,Status)
values (4,CONVERT(VARCHAR(19),GETDATE()),'New');
insert into PropAccomodation (Id_proposition,TypeOfRoom,BruttoPrice,Vat,Amount,Days)
select p.Id,'DoubleRoomEP',450,23,3,10  from Proposition p  where p.Id_user = 4;
insert into PropAccomodationDiscount ( Id_proposition, StandardPrice,Discount,DoubleRoomEP,BussinesSingleEP,ApartmentSingleEP)
select p.Id,100,20,45,100,220 from Proposition p where p.Id_user = 4;
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName, CustomerEmail)
select  p.Id,'Wytwórnia win','Jagodowa 3 23-765 Siemianowiec','1234556698','Sebastian Nalepka', 500295458,'Patrycja Kowalska' ,'sn@wp.pl'   from Proposition p;
insert into PropExtraServices(Id_proposition,ServiceType,BruttoPrice,Vat,Days)
select  p.Id,'Inne ',10,8,3  from Proposition p where p.Id_user = 4;
insert into PropExtraServicesDiscount(Id_proposition,StandardPrice,Discount)
select  p.Id,100,50  from Proposition p where p.Id_user = 4;
insert into PropHallEquipment(Id_proposition,Things,BruttoPrice,Vat,Amount,Days)
select  p.Id,'RZUTNIK',10,8,2,10  from Proposition p where p.Id_user = 4;
insert into PropHallEquipment(Id_proposition,Things,BruttoPrice,Vat,Amount,Days)
select  p.Id,'LAPTOP',110,8,2,10  from Proposition p where p.Id_user = 4;
insert into PropHallEquipmentDiscount(Id_proposition,StandardPrice,Discount) 
select p.Id,45,20 from Proposition p where p.Id_user = 4 ;
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża gastronomia 8%',80,'MG8' from Proposition p where p.Id_user = 4;


insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select p.Id,'Marża gastronomia 23%',50,'MG23'  from Proposition p where p.Id_user = 4;
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża alkohole',30,'MA'  from Proposition p where p.Id_user = 4;
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża napoje',20,'MN'  from Proposition p where p.Id_user = 4;


insert into PropMenuPosition(Id_proposition,TypeOfService,BruttoPrice,Vat,Amount,Days)
select  p.Id,'Lunch serwowany - 3 dania',80,8,3,4  from Proposition p where p.Id_user = 4;
insert into PropMenuPosition(Id_proposition,TypeOfService,BruttoPrice,Vat,Amount,Days) 
select  p.Id,'Lunch serwowany - Menu I Sląskie smaki',50,8,3,4  from Proposition p where p.Id_user = 4;
insert into PropMenuPosition(Id_proposition,TypeOfService,BruttoPrice,Vat,Amount,Days)
select  p.Id,'Wyborowa0,5 l',40,23,3,4 from Proposition p where p.Id_user = 4;
insert into PropPaymentSuggestions (Id_proposition,PaymentForm,IndividualOrders,InvoiceServiceName,CarPark,HallDescription)
select   p.Id,'Gotówka/karta','Doliczany do faktury','Posiłki grupowe HP','Brak','teatral' from Proposition p where p.Id_user = 4;
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select  p.Id,CONVERT(VARCHAR(19),GETDATE()), '10:15:00' ,CONVERT(VARCHAR(19),(GETDATE()+100)), '14:15:00',6,'C','Teatra' from Proposition p;

insert into Proposition (Id_user, UpdateDate,Status)
values (6,CONVERT(VARCHAR(19),GETDATE()+100),'New');
insert into PropAccomodation (Id_proposition,TypeOfRoom,BruttoPrice,Vat,Amount,Days)
select p.Id,'ApartmentSingleEP',750,23,4,30 from Proposition p  where p.Id_user = 6;
insert into PropAccomodationDiscount ( Id_proposition, StandardPrice,Discount,DoubleRoomEP,BussinesSingleEP,ApartmentSingleEP)
select p.Id,100,20,45,100,220 from Proposition p where p.Id_user = 6;
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName ,CustomerEmail)
select p.Id,'Przetwórstwo futra','Pożeczkowa 37 76-717 Bytom','1734573386','Mateusz Wolski', 634562926,'Ignacy Krasiński' ,'Test@test.pl'from Proposition p where p.Id_user = 6;
insert into PropExtraServices(Id_proposition,ServiceType,BruttoPrice,Vat,Days)
select p.Id,'Parking',2.50,8,3 from Proposition p where p.Id_user = 6;
insert into PropExtraServicesDiscount(Id_proposition,StandardPrice,Discount)
select p.Id,500,80 from Proposition p where p.Id_user = 6;
insert into PropHallEquipment(Id_proposition,Things,BruttoPrice,Vat,Amount,Days)
select p.Id,'EKRAN', 20,8,3,8 from Proposition p where p.Id_user = 6;
insert into PropHallEquipment(Id_proposition,Things,BruttoPrice,Vat,Amount,Days)
select p.Id,'RZYTNIK', 10,8,1,8 from Proposition p where p.Id_user = 6;
insert into PropHallEquipmentDiscount(Id_proposition,StandardPrice,Discount)
select p.Id,500,80 from Proposition p where p.Id_user=6;
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select p.Id,'Marża gastronomia 8%',70,'MG8' from Proposition p where p.Id_user = 6;


insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select p.Id,'Marża gastronomia 23%',40,'MG23' from Proposition p where p.Id_user = 6;
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select p.Id,'Marża alkohole',50,'MA' from Proposition p where p.Id_user = 6;
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select p.Id,'Marża napoje',20,'MN' from Proposition p where p.Id_user = 6;


insert into PropMenuPosition(Id_proposition,TypeOfService,BruttoPrice,Vat,Amount,Days)
select p.Id,'Lunch serwowany - 2 dania',60,8,6,2 from Proposition p where p.Id_user = 6;
insert into PropMenuPosition(Id_proposition,TypeOfService,BruttoPrice,Vat,Amount,Days)
select p.Id,'Lunch serwowany - Menu III Wspomnienia lata',30,8,6,2 from Proposition p where p.Id_user = 6;
insert into PropPaymentSuggestions (Id_proposition,PaymentForm,IndividualOrders,InvoiceServiceName,CarPark,HallDescription)
select p.Id,'Przelew 21 dni','Ryczałt','Usługa Konferencyjna 23%',Null,Null from Proposition p where p.Id_user = 6;
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select p.Id,CONVERT(VARCHAR(19),(GETDATE()+30)), '12:15:00' ,CONVERT(VARCHAR(19),(GETDATE()+55)), '17:15:00',6,'C','Teatra' from Proposition p where p.Id_user = 6;
