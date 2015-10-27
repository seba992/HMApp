use [DiamondDB]
insert into AccountPrivileges (Id,AccountType)
values (1,'a'),(2,'s');

insert into Users (Name,Surname,PhoneNum, Email,Position,AccountType,Login,Password)
values 
('Lidia' , 'Torzewska',	'512971553',	'ltorzewska@hotelediament.pl'	,'Regionalny Kierownik Sprzedaży',2, 'lito','test'),
('Agata', 'Kleszko',	'506102076',	'akleszko@hotelediament.pl','Specjalista ds. sprzedaży',2,'agkl','test'),
('Daria', 'Drewniok',	'506100952',	'ddrewniok@hotelediament.pl',	'Specjalista ds. sprzedaży',2,'dadr','test'),
('Paweł','Jaszczyk',	'512007210','pjaszczyk@hotelediament.pl',	'Specjalista ds. sprzedaży',2 , 'paja','test'),
( 'Michał',' Witkowicz',	'609060268',	'mwitkowicz@hotelediament.pl',	'Specjalista ds. sprzedaży',1,'miwi','test'),
('Tomasz', 'Antolak',	'603051705',	'tantolak@hotelediament.pl',	'Specjalista ds. sprzedaży',2,'toan','test'),
('Małgorzata', 'Michalak'	,'695219621',	'mmichalak@hotelediament.pl',	'Contracting Manager',2,'mami','test'),
('Tomasz', 'Sikora',	'512971529',	'tsikora@hotelediament.pl',	'Specjalista ds. Sprzedaży',2,'tosi','test'),
('Anna', 'Rosińska',	NULl,	'arosinska@hotelediament.pl',	'Specjalista ds. Sprzedaży',2,'anro','test'),
('Maria' ,'Bonk',	'506101535',	'mbonk@hotelediament.pl',	'Specjalista ds. Sprzedaży',2,'mabo','test'),
('Sylwia', 'Śliwka', '506101958',	'ssliwka@hotelediament.pl',	'Specjalista ds. Klienta wypoczynkowego',2,'sysl','test'),
('Joanna', 'Gwiazdowska',	'605882838',	'jgwiazdowska@hotelediament.pl','Specjalista ds. Sprzedaży MICE',2,'jogw','test'),
('Monika', 'Medwid',	'512971368',	'mmedwid@hotelediament.pl',	'Specjalista ds. Sprzedaży',2,'mome','test' ),
('Marcin', 'Budniok', 	'667671471', 	'marcin.budniok@stylehotels.pl',	'Kierownik Działu Sprzedaży',2,'mabu','test'),
('Daria', 'Drewniok',	'506100952',	'ddrewniok@hotelediament.pl',	'Specjalista ds. sprzedaży',2,'dadr','test'),
('Justyna', 'Majchrzak',	'512971568','jmajchrzak@hotelediament.pl',	'Specjalista ds. sprzedaży',2,'juma','test'),
('Magdalena', 'Jędruszek', 	'723550657',	'mjedruszek@hotelediament.pl',	'Specjalista ds. sprzedaży',2,'maje','test'),
('Magdalena', 'Radzikowska-Buchała',	'506101894',	'mradzikowska@hotelediament.pl',	'Specjalista ds. Sprzedaży',2,'mara','test' ),
('Magdalena', 'Szopa',	'512971427',	'mszopa@hotelediament.pl',	'Specjalista ds. Sprzedaży',2,'ma','sz' )

insert into Proposition (Id_user, UpdateDate)
values (4,CONVERT(VARCHAR(19),GETDATE()));
insert into PropAccomodation (Id_proposition,TypeOfRoom,BruttoPrice,Vat,Amount,Days)
select p.Id,'DoubleRoomEP',450,23,3,10  from Proposition p  where p.Id_user = 4;
insert into PropAccomodationDiscount ( Id_proposition, StandardPrice,Discount,DoubleRoomEP,BussinesSingleEP,BussinesDoubleEP,ApartmentSingleEP,ApartmentDoubleEP)
select p.Id,100,20,45,100,145,220,265  from Proposition p where p.Id_user = 4;
insert into PropClient (Id_proposition,CompanyName,CompanyStreet,CompanyBuildingNumber,CompanyZipCode,CompanyCity,NIP,CustomerName,CustomerSurname,PhoneNum,DecisingPersonName,DecisingPersonSurname)
select  p.Id,'Wytwórnia win','Jagodowa',3,'23-765','Siemianowiec','1234556698','Sebastian','Nalepka', 500295458,'Patrycja','Kowalska'  from Proposition p;
insert into PropExtraServices(Id_proposition,ServiceType,BruttoHourPrice,Vat,Days)
select  p.Id,'Inne ',10,8,3  from Proposition p where p.Id_user = 4;
insert into PropExtraServicesDiscount(Id_proposition,StandardPrice,Discount)
select  p.Id,100,50  from Proposition p where p.Id_user = 4;
insert into PropHallEquipment(Id_proposition,Things,BruttoPrice,Vat,Amount,Days)
select  p.Id,'Rzutnik',10,8,2,10  from Proposition p where p.Id_user = 4;
insert into PropHallEquipment(Id_proposition,Things,BruttoPrice,Vat,Amount,Days)
select  p.Id,'Laptop',110,8,2,10  from Proposition p where p.Id_user = 4;
insert into PropHallEquipmentDiscount(Id,StandardPrice,Discount) 
select ph.Id,45,20 from PropHallEquipment ph where ph.Things  LIKE 'Laptop';
insert into PropHallEquipmentDiscount(Id,StandardPrice,Discount)
select ph.Id,30,10  from PropHallEquipment ph where ph.Things  LIKE 'Rzutnik';
insert into PropMenuMarge(Id_proposition,MargeName,DefaultValue,MargeType)
select  p.Id,'Marża gastronomia 8%',80,'MG8' from Proposition p where p.Id_user = 4;
insert into PropMenuMarge(Id_proposition,MargeName,DefaultValue,MargeType)
select p.Id,'Marża gastronomia 23%',50,'MG23'  from Proposition p where p.Id_user = 4;
insert into PropMenuMarge(Id_proposition,MargeName,DefaultValue,MargeType)
select  p.Id,'Marża alkohole',30,'MA'  from Proposition p where p.Id_user = 4;
insert into PropMenuMarge(Id_proposition,MargeName,DefaultValue,MargeType)
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

insert into Proposition (Id_user, UpdateDate)
values (6,CONVERT(VARCHAR(19),GETDATE()+100));
insert into PropAccomodation (Id_proposition,TypeOfRoom,BruttoPrice,Vat,Amount,Days)
select p.Id,'ApartmentSingleEP',750,23,4,30 from Proposition p  where p.Id_user = 6;
insert into PropAccomodationDiscount ( Id_proposition, StandardPrice,Discount,DoubleRoomEP,BussinesSingleEP,BussinesDoubleEP,ApartmentSingleEP,ApartmentDoubleEP)
select p.Id,100,20,45,100,145,220,265 from Proposition p where p.Id_user = 6;
insert into PropClient (Id_proposition,CompanyName,CompanyStreet,CompanyBuildingNumber,CompanyZipCode,CompanyCity,NIP,CustomerName,CustomerSurname,PhoneNum,DecisingPersonName,DecisingPersonSurname)
select p.Id,'Przetwórstwo futra','Pożeczkowa',37,'76-717','Bytom','1734573386','Mateusz','Wolski', 634562926,'Ignacy','Krasiński'from Proposition p;
insert into PropExtraServices(Id_proposition,ServiceType,BruttoHourPrice,Vat,Days)
select p.Id,'Parking',2.50,8,3 from Proposition p where p.Id_user = 6;
insert into PropExtraServicesDiscount(Id_proposition,StandardPrice,Discount)
select p.Id,500,80 from Proposition p where p.Id_user = 6;
insert into PropHallEquipment(Id_proposition,Things,BruttoPrice,Vat,Amount,Days)
select p.Id,'Ekran', 20,8,3,8 from Proposition p where p.Id_user = 6;
insert into PropHallEquipment(Id_proposition,Things,BruttoPrice,Vat,Amount,Days)
select p.Id,'Rzutnik', 10,8,1,8 from Proposition p where p.Id_user = 6;
insert into PropHallEquipmentDiscount(Id,StandardPrice,Discount)
select ph.Id,500,80 from PropHallEquipment ph where ph.Things LIKE 'Ekran' ;
insert into PropHallEquipmentDiscount(Id,StandardPrice,Discount)
select ph.Id,20,35 from PropHallEquipment ph where ph.Things LIKE 'Rzutnik' ;
insert into PropMenuMarge(Id_proposition,MargeName,DefaultValue,MargeType)
select p.Id,'Marża gastronomia 8%',70,'MG8' from Proposition p where p.Id_user = 6;
insert into PropMenuMarge(Id_proposition,MargeName,DefaultValue,MargeType)
select p.Id,'Marża gastronomia 23%',40,'MG23' from Proposition p where p.Id_user = 6;
insert into PropMenuMarge(Id_proposition,MargeName,DefaultValue,MargeType)
select p.Id,'Marża alkohole',50,'MA' from Proposition p where p.Id_user = 6;
insert into PropMenuMarge(Id_proposition,MargeName,DefaultValue,MargeType)
select p.Id,'Marża napoje',20,'MN' from Proposition p where p.Id_user = 6;
insert into PropMenuPosition(Id_proposition,TypeOfService,BruttoPrice,Vat,Amount,Days)
select p.Id,'Lunch serwowany - 2 dania',60,8,6,2 from Proposition p where p.Id_user = 6;
insert into PropMenuPosition(Id_proposition,TypeOfService,BruttoPrice,Vat,Amount,Days)
select p.Id,'Lunch serwowany - Menu III Wspomnienia lata',30,8,6,2 from Proposition p where p.Id_user = 6;
insert into PropPaymentSuggestions (Id_proposition,PaymentForm,IndividualOrders,InvoiceServiceName,CarPark,HallDescription)
select p.Id,'Przelew 21 dni','Ryczałt','Usługa Konferencyjna 23%',Null,Null from Proposition p where p.Id_user = 6;
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select p.Id,CONVERT(VARCHAR(19),(GETDATE()+30)), '12:15:00' ,CONVERT(VARCHAR(19),(GETDATE()+55)), '17:15:00',6,'C','Teatra' from Proposition p where p.Id_user = 6;
