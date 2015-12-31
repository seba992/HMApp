use [DiamondDB]
insert into AccountPrivileges (Id,AccountType)
values (1,'a'),(2,'s');

insert into Users (Name,Surname,PhoneNum, Email,Position,AccountType,Login,Password,FirstLogin)
values 
('Tomasz' , 'Karolak',	'595584695',	'tkarol234@gmail.com'	,'Menadżer',1, 'toko','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','t'),
('Agata', 'Przybysz',	'958612357',	'agataprzybysz@gmail.com','Specjalista ds. sprzedaży',2,'agpr','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','t'),
('Daria', 'Warcab',		'951753654',	'warcabdaria@gmail.com',	'Specjalista ds. sprzedaży',2,'dawa','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','t'),
('Paweł','Lis',			'564521654',	'plis76@wp.pl',	'Specjalista ds. sprzedaży',2 , 'pali','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','t'),
('Michał','Komada',		'756489214',	'mkomada@wp.pl',	'Specjalista ds. sprzedaży',1,'miko','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','t'),
('Tomasz', 'Wiglusz',	'654812384',	'tantolak@wp.pl',	'Specjalista ds. sprzedaży',2,'towi','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','t'),
('Małgorzata', 'Rembisz','698546458',	'mmichalak@onet.pl',	'Dyrektor',1,'mare','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','t'),
('Tomasz', 'Skowronek',	'475856185',	'tsikora@onet.pl',	'Specjalista ds. Sprzedaży',2,'tosk','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','t'),
('Anna', 'Rosińska',		NULL,		'arosinska@onet.pl',	'Specjalista ds. Sprzedaży',2,'anro','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','t'),
('Maria' ,'Krzemień',	'846484184',	'm_krzemień@interia.pl',	'Specjalista ds. Sprzedaży',2,'makr','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','t'),
('Sylwia', 'Gruszka', 	'557487748',	'sgruszka785@interia.pl',	'Specjalista ds. Sprzedaży',2,'sygr','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','t'),
('Joanna', 'Ulman',		'456861564',	'joanna_ulman@interia.pl','Specjalista ds. Sprzedaży',2,'joul','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','t'),
('Monika', 'Duda',		'846843182',	'mduda@gmail.pl',	'Specjalista ds. Sprzedaży',2,'modu','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','t' );

declare @iduser int;
select @iduser =  id from Users where Login ='toko';

begin
insert into Proposition (Id_user, UpdateDate,Status)
values (@iduser,CONVERT(VARCHAR(19),GETDATE()),'Nowa');
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName, CustomerEmail)
select  p.Id,'Wytwórnia win','Jagodowa 3 23-765 Siemianowiec','1234556698','Łukasz Lis', 500295458,'Łukasz Lis' ,'sn@wp.pl'   from Proposition p;
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select  p.Id,CONVERT(VARCHAR(19),2015-12-29), '10:15:00' ,CONVERT(VARCHAR(19),2015-12-29), '14:15:00',6,'A',NULL from Proposition p;
insert into PropHallEquipment(Id_proposition,Things,BruttoPrice,Vat,Amount,Days)
select  p.Id,'Sala A',285,23,3,4  from Proposition p where p.Id_user = @iduser;
insert into PropHallEquipmentDiscount(Id_proposition,StandardPrice,Discount) 
select p.Id,300,5 from Proposition p where p.Id_user = @iduser ;
insert into PropMenuPosition(Id_proposition,TypeOfService,BruttoPrice,Vat,Amount,Days)
select  p.Id,'Lunch serwowany - 2 dania z przystawką',63.1401443,8,2,3  from Proposition p where p.Id_user = @iduser;

insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża gastronomia 8%)',77,'MGA8' from Proposition p where p.Id_user = @iduser;
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select p.Id,'MMarża gastronomia 23%)',78,'MGA23'  from Proposition p where p.Id_user = @iduser;
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża Alkohole)',78,'MALK'  from Proposition p where p.Id_user = @iduser;
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża niskobudżetowe)',58,'MNIS'  from Proposition p where p.Id_user = @iduser;
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Napoje)',48,'MNAPO'  from Proposition p where p.Id_user = @iduser;

insert into PropAccomodation (Id_proposition,TypeOfRoom,BruttoPrice,Vat,Amount,Days)
select p.Id,'POKÓJ 1-OSOBOWY',242.5,8,3,3  from Proposition p  where p.Id_user = @iduser;
insert into PropAccomodationDiscount ( Id_proposition, StandardPrice,Discount,DoubleRoomEP,BussinesSingleEP,ApartmentSingleEP)
select p.Id,NULL,3,NULL,NULL,NULL from Proposition p where p.Id_user = @iduser;
insert into PropExtraServices(Id_proposition,ServiceType,BruttoPrice,Vat,Days,Amount)
select  p.Id,'PARKING ( część dozorowana )',3.5,8,3,3  from Proposition p where p.Id_user = @iduser;
insert into PropExtraServices(Id_proposition,ServiceType,BruttoPrice,Vat,Days,Amount)
select  p.Id,'Inne',5,8,3,3  from Proposition p where p.Id_user = @iduser;
insert into PropPaymentSuggestions (Id_proposition,PaymentForm,IndividualOrders,InvoiceServiceName,CarPark,HallDescription)
select   p.Id,'100% do dnia realizacji','Doliczany do faktury','Gastronomia konferencyjna 8% ; Gastronomia konferencyjna 23% ; Wynajem Sali 23% ; Usługa noclegowa 8%','Płatny indywidualnie',null from Proposition p where p.Id_user = @iduser;
end







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
