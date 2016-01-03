use [DiamondDB]
insert into AccountPrivileges (Id,AccountType)
values (1,'a'),(2,'s');

insert into Users (Name,Surname,PhoneNum, Email,Position,AccountType,Login,Password,FirstLogin)
values 
('Tomasz' , 'Karolak',	'595584695',	'tkarol234@gmail.com'	,'Menadżer',1, 'tomasz.karolak','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','f'),
('Agata', 'Przybysz',	'958612357',	'agataprzybysz@gmail.com','Specjalista ds. sprzedaży',2,'agata.przybysz','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','f'),
('Daria', 'Warcab',		'951753654',	'warcabdaria@gmail.com',	'Specjalista ds. sprzedaży',2,'daria.warcab','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','f'),
('Paweł','Lis',			'564521654',	'plis76@wp.pl',	'Specjalista ds. sprzedaży',2 , 'pawel.lis','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','f'),
('Michał','Wilert',		'756489214',	'mkomada@wp.pl',	'Specjalista ds. sprzedaży',1,'miwi','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','f'),
('Tomasz', 'Wiglusz',	'654812384',	'tantolak@wp.pl',	'Specjalista ds. sprzedaży',2,'tomasz.wiglusz','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','f'),
('Małgorzata', 'Rembisz','698546458',	'mmichalak@onet.pl',	'Dyrektor',1,'malgorzata.rembisz','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','f'),
('Tomasz', 'Skowronek',	'475856185',	'tsikora@onet.pl',	'Specjalista ds. Sprzedaży',2,'tomasz.skowronek','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','f'),
('Anna', 'Rosińska',		NULL,		'arosinska@onet.pl',	'Specjalista ds. Sprzedaży',2,'anna.rosinska','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','f'),
('Maria' ,'Krzemień',	'846484184',	'm_krzemień@interia.pl',	'Specjalista ds. Sprzedaży',2,'maria.krzemien','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','f'),
('Sylwia', 'Gruszka', 	'557487748',	'sgruszka785@interia.pl',	'Specjalista ds. Sprzedaży',2,'sylwia.gruszka','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','f'),
('Joanna', 'Ulman',		'456861564',	'joanna_ulman@interia.pl','Specjalista ds. Sprzedaży',2,'joanna.ulman','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','f'),
('Monika', 'Duda',		'846843182',	'mduda@gmail.pl',	'Specjalista ds. Sprzedaży',2,'monika.duda','9b971780f63d4d90eb7475ed1e365ba78559f7b2fd557f1d2650f92e10d7f8e1','f' );

declare @iduser int;
select @iduser =  id from Users where Login ='tomasz.karolak';

begin
insert into Proposition (Id_user, UpdateDate,Status)
values (@iduser,CONVERT(VARCHAR(19),GETDATE() - 60),'Nowa');
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName, CustomerEmail)
select  p.Id,'Wytwórnia win','ul. Jagodowa 3 23-765 Siemianowiec','1234556698','Łukasz Lis', 500295458,'Łukasz Lis' ,'sn@wp.pl'   from Proposition p where p.Id_user = @iduser;
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select  p.Id,CONVERT(VARCHAR(19),2015-12-29), '10:15:00' ,CONVERT(VARCHAR(19),2015-12-29), '14:15:00',6,'A',NULL from Proposition p where p.Id_user = @iduser;
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

--************************************************** druga

select @iduser =  id from Users where Login ='agata.przybysz';
PRINT @iduser 
insert into Proposition (Id_user, UpdateDate,Status)
values (@iduser,CONVERT(VARCHAR(19),GETDATE() - 70),'Nowa');
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName, CustomerEmail)
select  p.Id,'Reserved','ul. Francuska 32/43 23-765 Bytom','1234556698','Iwona Janioł', 852456951,'Iwona Janioł' ,'iwonaj@reserved.pl'   from Proposition p  where p.Id_user = @iduser;
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select  p.Id,CONVERT(VARCHAR(19),2015-12-29), '10:15:00' ,CONVERT(VARCHAR(19),2015-12-29), '14:15:00',6,'A',NULL from Proposition p  where p.Id_user = @iduser;
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

-- *****************************************************kolejna
select @iduser =  id from Users where Login ='daria.warcab';
PRINT @iduser 
insert into Proposition (Id_user, UpdateDate,Status)
values (@iduser,CONVERT(VARCHAR(19),GETDATE() - 30),'Nowa');
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName, CustomerEmail)
select  p.Id,'Adidas','ul. Chorzowska 324 95-755 Warszawa','9875656698','Kamil Duda', 852456951,'Kamil Duda' ,'kamil_duda@adidas.pl'   from Proposition p  where p.Id_user = @iduser;
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select  p.Id,CONVERT(VARCHAR(19),2015-12-29), '10:15:00' ,CONVERT(VARCHAR(19),2015-12-29), '14:15:00',6,'A',NULL from Proposition p  where p.Id_user = @iduser;
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

-- *****************************************************kolejna
select @iduser =  id from Users where Login ='pawel.lis';
PRINT @iduser 
insert into Proposition (Id_user, UpdateDate,Status)
values (@iduser,CONVERT(VARCHAR(19),GETDATE() - 33),'Nowa');
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName, CustomerEmail)
select  p.Id,'Google','ul. Powstańców Warszawskich 54/2 95-755 Wrocław','9875656698','Iwona Sapia', 852456951,'Kamil Duda' ,'iwonaiapia@google.com'   from Proposition p  where p.Id_user = @iduser;
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select  p.Id,CONVERT(VARCHAR(19),2015-12-29), '10:15:00' ,CONVERT(VARCHAR(19),2015-12-29), '14:15:00',6,'A',NULL from Proposition p where p.Id_user = @iduser;
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


-- *****************************************************kolejna
select @iduser =  id from Users where Login ='miwi';
PRINT @iduser 
insert into Proposition (Id_user, UpdateDate,Status)
values (@iduser,CONVERT(VARCHAR(19),GETDATE() - 44 ),'Nowa');
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName, CustomerEmail)
select  p.Id,'Sklep wielobranżowy','ul. Gajowa 43/2 95-755 Łódz','9875656698','Huberd Zboroń', 852456951,'Huberd Zboroń' ,'huber2321@wp.pl'   from Proposition p  where p.Id_user = @iduser;
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select  p.Id,CONVERT(VARCHAR(19),2015-12-29), '10:15:00' ,CONVERT(VARCHAR(19),2015-12-29), '14:15:00',6,'A',NULL from Proposition p  where p.Id_user = @iduser;
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


-- *****************************************************kolejna
select @iduser =  id from Users where Login ='tomasz.wiglusz';
PRINT @iduser 
insert into Proposition (Id_user, UpdateDate,Status)
values (@iduser,CONVERT(VARCHAR(19),GETDATE() - 65),'Nowa');
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName, CustomerEmail)
select  p.Id,'Sklep spożywczy','ul. Łagienicka 253/2 58-785 Łódz','9875656698','Huberd Zboroń', 852456951,'Huberd Zboroń' ,'huber2321@wp.pl'   from Proposition p  where p.Id_user = @iduser;
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select  p.Id,CONVERT(VARCHAR(19),2015-12-29), '10:15:00' ,CONVERT(VARCHAR(19),2015-12-29), '14:15:00',6,'A',NULL from Proposition p  where p.Id_user = @iduser;
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

-- *****************************************************kolejna
select @iduser =  id from Users where Login ='anna.rosinska';
PRINT @iduser 
insert into Proposition (Id_user, UpdateDate,Status)
values (@iduser,CONVERT(VARCHAR(19),GETDATE() - 20),'Nowa');
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName, CustomerEmail)
select  p.Id,'Mechanik','ul. Opolska 23/32 53-783 Poznań','9875656698','Jan Podolski', 852456951,'Jan Podolski' ,'janp2321@wp.pl'   from Proposition p  where p.Id_user = @iduser;
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select  p.Id,CONVERT(VARCHAR(19),2015-12-29), '10:15:00' ,CONVERT(VARCHAR(19),2015-12-29), '14:15:00',6,'A',NULL from Proposition p  where p.Id_user = @iduser;
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

-- *****************************************************kolejna
select @iduser =  id from Users where Login ='tomasz.skowronek';
PRINT @iduser 
insert into Proposition (Id_user, UpdateDate,Status)
values (@iduser,CONVERT(VARCHAR(19),GETDATE() - 10),'Nowa');
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName, CustomerEmail)
select  p.Id,'Dentysta "Piękny uśmiech"','ul. Wrocławska 3/2 53-783 Gdańsk','9875656698','Karolina Komada ', 852456951,'Karolina Komada' ,'komada997@wp.pl'   from Proposition p  where p.Id_user = @iduser;
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select  p.Id,CONVERT(VARCHAR(19),2015-12-29), '10:15:00' ,CONVERT(VARCHAR(19),2015-12-29), '14:15:00',6,'A',NULL from Proposition p  where p.Id_user = @iduser;
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


-- *****************************************************kolejna
select @iduser =  id from Users where Login ='malgorzata.rembisz';
PRINT @iduser 
insert into Proposition (Id_user, UpdateDate,Status)
values (@iduser,CONVERT(VARCHAR(19),GETDATE() - 30),'Nowa');
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName, CustomerEmail)
select  p.Id,'Piekarnia','ul. Wrocławska 3/2 53-783 Gdańsk','9875656698','Łukasz Śienkiewicz', 852456951,'Łukasz Śienkiewicz' ,'komada997@wp.pl'   from Proposition p  where p.Id_user = @iduser;
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select  p.Id,CONVERT(VARCHAR(19),2015-12-29), '10:15:00' ,CONVERT(VARCHAR(19),2015-12-29), '14:15:00',6,'A',NULL from Proposition p  where p.Id_user = @iduser;
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


-- *****************************************************kolejna
select @iduser =  id from Users where Login ='monika.duda';
PRINT @iduser 
insert into Proposition (Id_user, UpdateDate,Status)
values (@iduser,CONVERT(VARCHAR(19),GETDATE() - 44),'Nowa');
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName, CustomerEmail)
select  p.Id,'Cukiernia','ul. Wrocławska 3/2 53-783 Gdańsk','9875656698','Michał Sałata', 852456951,'Michał Sałata' ,'komada997@wp.pl'   from Proposition p  where p.Id_user = @iduser;
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select  p.Id,CONVERT(VARCHAR(19),2015-12-29), '10:15:00' ,CONVERT(VARCHAR(19),2015-12-29), '14:15:00',6,'A',NULL from Proposition p  where p.Id_user = @iduser;
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



-- *****************************************************kolejna
select @iduser =  id from Users where Login ='joanna.ulman';
PRINT @iduser 
insert into Proposition (Id_user, UpdateDate,Status)
values (@iduser,CONVERT(VARCHAR(19),GETDATE() - 90),'Nowa');
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName, CustomerEmail)
select  p.Id,'Cukiernia','ul. Wrocławska 3/2 53-783 Gdańsk','9875656698','Anna Simon', 852456951,'Anna Simon' ,'komada997@wp.pl'   from Proposition p  where p.Id_user = @iduser;
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select  p.Id,CONVERT(VARCHAR(19),2015-12-29), '10:15:00' ,CONVERT(VARCHAR(19),2015-12-29), '14:15:00',6,'A',NULL from Proposition p  where p.Id_user = @iduser;
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



-- *****************************************************kolejna
select @iduser =  id from Users where Login ='maria.krzemien';
PRINT @iduser 
insert into Proposition (Id_user, UpdateDate,Status)
values (@iduser,CONVERT(VARCHAR(19),GETDATE() - 10),'Nowa');
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName, CustomerEmail)
select  p.Id,'Skład węgla','ul. Wrocławska 3/2 53-783 Gdańsk','9875656698','Mateusz Mularczyk', 852456951,'Mateusz Mularczyk' ,'komada997@wp.pl'   from Proposition p  where p.Id_user = @iduser;
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select  p.Id,CONVERT(VARCHAR(19),2015-12-29), '10:15:00' ,CONVERT(VARCHAR(19),2015-12-29), '14:15:00',6,'A',NULL from Proposition p  where p.Id_user = @iduser;
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


-- *****************************************************kolejna
select @iduser =  id from Users where Login ='sylwia.gruszka';
PRINT @iduser 
insert into Proposition (Id_user, UpdateDate,Status)
values (@iduser,CONVERT(VARCHAR(19),GETDATE() - 20),'Nowa');
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName, CustomerEmail)
select  p.Id,'Firma budowlana','ul. Wrocławska 3/2 53-783 Gdańsk','9875656698','Jacek Wołczyk', 852456951,'Jacek Wołczyk' ,'komada997@wp.pl'   from Proposition p  where p.Id_user = @iduser;
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select  p.Id,CONVERT(VARCHAR(19),2015-12-29), '10:15:00' ,CONVERT(VARCHAR(19),2015-12-29), '14:15:00',6,'A',NULL from Proposition p  where p.Id_user = @iduser;
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

--*************************************************************
select @iduser =  id from Users where Login ='joanna.ulman';
PRINT @iduser 
insert into Proposition (Id_user, UpdateDate,Status)
values (@iduser,CONVERT(VARCHAR(19),GETDATE() - 1),'W trakcie realizacji');
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName, CustomerEmail)
select  p.Id,'Zegarmistrz','ul. Wrocławska 3/2 53-783 Gdańsk','9875656698','Dawid Szczepanik', 852456951,'Dawid Szczepani' ,'komada997@wp.pl'   from Proposition p  where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select  p.Id,CONVERT(VARCHAR(19),2015-12-29), '10:15:00' ,CONVERT(VARCHAR(19),2015-12-29), '14:15:00',6,'A',NULL from Proposition p  where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropHallEquipment(Id_proposition,Things,BruttoPrice,Vat,Amount,Days)
select  p.Id,'Sala A',285,23,3,4  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropHallEquipmentDiscount(Id_proposition,StandardPrice,Discount) 
select p.Id,300,5 from Proposition p where p.Id_user = @iduser  and p.Status='W trakcie realizacji';
insert into PropMenuPosition(Id_proposition,TypeOfService,BruttoPrice,Vat,Amount,Days)
select  p.Id,'Lunch serwowany - 2 dania z przystawką',63.1401443,8,2,3  from Proposition p where p.Id_user = @iduser  and p.Status='W trakcie realizacji';

insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża gastronomia 8%)',77,'MGA8' from Proposition p where p.Id_user = @iduser  and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select p.Id,'MMarża gastronomia 23%)',78,'MGA23'  from Proposition p where p.Id_user = @iduser  and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża Alkohole)',78,'MALK'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża niskobudżetowe)',58,'MNIS'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Napoje)',48,'MNAPO'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';

insert into PropAccomodation (Id_proposition,TypeOfRoom,BruttoPrice,Vat,Amount,Days)
select p.Id,'POKÓJ 1-OSOBOWY',242.5,8,3,3  from Proposition p  where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropAccomodationDiscount ( Id_proposition, StandardPrice,Discount,DoubleRoomEP,BussinesSingleEP,ApartmentSingleEP)
select p.Id,NULL,3,NULL,NULL,NULL from Proposition p where p.Id_user = @iduser  and p.Status='W trakcie realizacji';
insert into PropExtraServices(Id_proposition,ServiceType,BruttoPrice,Vat,Days,Amount)
select  p.Id,'PARKING ( część dozorowana )',3.5,8,3,3  from Proposition p where p.Id_user = @iduser  and p.Status='W trakcie realizacji';
insert into PropExtraServices(Id_proposition,ServiceType,BruttoPrice,Vat,Days,Amount)
select  p.Id,'Inne',5,8,3,3  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropPaymentSuggestions (Id_proposition,PaymentForm,IndividualOrders,InvoiceServiceName,CarPark,HallDescription)
select   p.Id,'100% do dnia realizacji','Doliczany do faktury','Gastronomia konferencyjna 8% ; Gastronomia konferencyjna 23% ; Wynajem Sali 23% ; Usługa noclegowa 8%','Płatny indywidualnie',null from Proposition p where p.Id_user = @iduser;


--*************************************************************
select @iduser =  id from Users where Login ='monika.duda';
PRINT @iduser 
insert into Proposition (Id_user, UpdateDate,Status)
values (@iduser,CONVERT(VARCHAR(19),GETDATE() - 10 ),'W trakcie realizacji');
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName, CustomerEmail)
select  p.Id,'KWK Bobrek','ul. Wrocławska 3/2 53-783 Gdańsk','9875656698','Herbert Salwicek', 852456951,'Herbert Salwicek' ,'komada997@wp.pl'   from Proposition p  where p.Id_user = @iduser  and p.Status='W trakcie realizacji';
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select  p.Id,CONVERT(VARCHAR(19),2015-12-29), '10:15:00' ,CONVERT(VARCHAR(19),2015-12-29), '14:15:00',6,'A',NULL from Proposition p  where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropHallEquipment(Id_proposition,Things,BruttoPrice,Vat,Amount,Days)
select  p.Id,'Sala A',285,23,3,4  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropHallEquipmentDiscount(Id_proposition,StandardPrice,Discount) 
select p.Id,300,5 from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuPosition(Id_proposition,TypeOfService,BruttoPrice,Vat,Amount,Days)
select  p.Id,'Lunch serwowany - 2 dania z przystawką',63.1401443,8,2,3  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';

insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża gastronomia 8%)',77,'MGA8' from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select p.Id,'MMarża gastronomia 23%)',78,'MGA23'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża Alkohole)',78,'MALK'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża niskobudżetowe)',58,'MNIS'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Napoje)',48,'MNAPO'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';

insert into PropAccomodation (Id_proposition,TypeOfRoom,BruttoPrice,Vat,Amount,Days)
select p.Id,'POKÓJ 1-OSOBOWY',242.5,8,3,3  from Proposition p  where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropAccomodationDiscount ( Id_proposition, StandardPrice,Discount,DoubleRoomEP,BussinesSingleEP,ApartmentSingleEP)
select p.Id,NULL,3,NULL,NULL,NULL from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropExtraServices(Id_proposition,ServiceType,BruttoPrice,Vat,Days,Amount)
select  p.Id,'PARKING ( część dozorowana )',3.5,8,3,3  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropExtraServices(Id_proposition,ServiceType,BruttoPrice,Vat,Days,Amount)
select  p.Id,'Inne',5,8,3,3  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropPaymentSuggestions (Id_proposition,PaymentForm,IndividualOrders,InvoiceServiceName,CarPark,HallDescription)
select   p.Id,'100% do dnia realizacji','Doliczany do faktury','Gastronomia konferencyjna 8% ; Gastronomia konferencyjna 23% ; Wynajem Sali 23% ; Usługa noclegowa 8%','Płatny indywidualnie',null from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';


--*************************************************************
select @iduser =  id from Users where Login ='tomasz.karolak';
PRINT @iduser 
insert into Proposition (Id_user, UpdateDate,Status)
values (@iduser,CONVERT(VARCHAR(19),GETDATE() - 20),'W trakcie realizacji');
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName, CustomerEmail)
select  p.Id,'Huta stali','ul. Wrocławska 3/2 53-783 Gdańsk','9875656698','Jan Poloczek', 852456951,'Jan Poloczek' ,'komada997@wp.pl'   from Proposition p  where p.Id_user = @iduser  and p.Status='W trakcie realizacji';
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select  p.Id,CONVERT(VARCHAR(19),2015-12-29), '10:15:00' ,CONVERT(VARCHAR(19),2015-12-29), '14:15:00',6,'A',NULL from Proposition p  where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropHallEquipment(Id_proposition,Things,BruttoPrice,Vat,Amount,Days)
select  p.Id,'Sala A',285,23,3,4  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropHallEquipmentDiscount(Id_proposition,StandardPrice,Discount) 
select p.Id,300,5 from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuPosition(Id_proposition,TypeOfService,BruttoPrice,Vat,Amount,Days)
select  p.Id,'Lunch serwowany - 2 dania z przystawką',63.1401443,8,2,3  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';

insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża gastronomia 8%)',77,'MGA8' from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select p.Id,'MMarża gastronomia 23%)',78,'MGA23'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża Alkohole)',78,'MALK'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża niskobudżetowe)',58,'MNIS'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Napoje)',48,'MNAPO'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';

insert into PropAccomodation (Id_proposition,TypeOfRoom,BruttoPrice,Vat,Amount,Days)
select p.Id,'POKÓJ 1-OSOBOWY',242.5,8,3,3  from Proposition p  where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropAccomodationDiscount ( Id_proposition, StandardPrice,Discount,DoubleRoomEP,BussinesSingleEP,ApartmentSingleEP)
select p.Id,NULL,3,NULL,NULL,NULL from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropExtraServices(Id_proposition,ServiceType,BruttoPrice,Vat,Days,Amount)
select  p.Id,'PARKING ( część dozorowana )',3.5,8,3,3  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropExtraServices(Id_proposition,ServiceType,BruttoPrice,Vat,Days,Amount)
select  p.Id,'Inne',5,8,3,3  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropPaymentSuggestions (Id_proposition,PaymentForm,IndividualOrders,InvoiceServiceName,CarPark,HallDescription)
select   p.Id,'100% do dnia realizacji','Doliczany do faktury','Gastronomia konferencyjna 8% ; Gastronomia konferencyjna 23% ; Wynajem Sali 23% ; Usługa noclegowa 8%','Płatny indywidualnie',null from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';

--*************************************************************
select @iduser =  id from Users where Login ='tomasz.skowronek';
PRINT @iduser 
insert into Proposition (Id_user, UpdateDate,Status)
values (@iduser,CONVERT(VARCHAR(19),GETDATE() - 50),'W trakcie realizacji');
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName, CustomerEmail)
select  p.Id,'Serwis komputerowy','ul. Wrocławska 3/2 53-783 Gdańsk','9875656698','Jan Bochenek', 852456951,'Jan Bochenek' ,'komada997@wp.pl'   from Proposition p  where p.Id_user = @iduser  and p.Status='W trakcie realizacji';
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select  p.Id,CONVERT(VARCHAR(19),2015-12-29), '10:15:00' ,CONVERT(VARCHAR(19),2015-12-29), '14:15:00',6,'A',NULL from Proposition p  where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropHallEquipment(Id_proposition,Things,BruttoPrice,Vat,Amount,Days)
select  p.Id,'Sala A',285,23,3,4  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropHallEquipmentDiscount(Id_proposition,StandardPrice,Discount) 
select p.Id,300,5 from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuPosition(Id_proposition,TypeOfService,BruttoPrice,Vat,Amount,Days)
select  p.Id,'Lunch serwowany - 2 dania z przystawką',63.1401443,8,2,3  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';

insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża gastronomia 8%)',77,'MGA8' from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select p.Id,'MMarża gastronomia 23%)',78,'MGA23'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża Alkohole)',78,'MALK'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża niskobudżetowe)',58,'MNIS'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Napoje)',48,'MNAPO'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';

insert into PropAccomodation (Id_proposition,TypeOfRoom,BruttoPrice,Vat,Amount,Days)
select p.Id,'POKÓJ 1-OSOBOWY',242.5,8,3,3  from Proposition p  where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropAccomodationDiscount ( Id_proposition, StandardPrice,Discount,DoubleRoomEP,BussinesSingleEP,ApartmentSingleEP)
select p.Id,NULL,3,NULL,NULL,NULL from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropExtraServices(Id_proposition,ServiceType,BruttoPrice,Vat,Days,Amount)
select  p.Id,'PARKING ( część dozorowana )',3.5,8,3,3  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropExtraServices(Id_proposition,ServiceType,BruttoPrice,Vat,Days,Amount)
select  p.Id,'Inne',5,8,3,3  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropPaymentSuggestions (Id_proposition,PaymentForm,IndividualOrders,InvoiceServiceName,CarPark,HallDescription)
select   p.Id,'100% do dnia realizacji','Doliczany do faktury','Gastronomia konferencyjna 8% ; Gastronomia konferencyjna 23% ; Wynajem Sali 23% ; Usługa noclegowa 8%','Płatny indywidualnie',null from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';


--*************************************************************
select @iduser =  id from Users where Login ='malgorzata.rembisz';
PRINT @iduser 
insert into Proposition (Id_user, UpdateDate,Status)
values (@iduser,CONVERT(VARCHAR(19),GETDATE() - 26),'W trakcie realizacji');
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName, CustomerEmail)
select  p.Id,'Zakład wulkanizacyjny','ul. Wrocławska 3/2 53-783 Gdańsk','9875656698','Tomasz Gwarecki', 852456951,'Tomasz Gwarecki' ,'komada997@wp.pl'   from Proposition p  where p.Id_user = @iduser  and p.Status='W trakcie realizacji';
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select  p.Id,CONVERT(VARCHAR(19),2015-12-29), '10:15:00' ,CONVERT(VARCHAR(19),2015-12-29), '14:15:00',6,'A',NULL from Proposition p  where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropHallEquipment(Id_proposition,Things,BruttoPrice,Vat,Amount,Days)
select  p.Id,'Sala A',285,23,3,4  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropHallEquipmentDiscount(Id_proposition,StandardPrice,Discount) 
select p.Id,300,5 from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuPosition(Id_proposition,TypeOfService,BruttoPrice,Vat,Amount,Days)
select  p.Id,'Lunch serwowany - 2 dania z przystawką',63.1401443,8,2,3  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';

insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża gastronomia 8%)',77,'MGA8' from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select p.Id,'MMarża gastronomia 23%)',78,'MGA23'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża Alkohole)',78,'MALK'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża niskobudżetowe)',58,'MNIS'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Napoje)',48,'MNAPO'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';

insert into PropAccomodation (Id_proposition,TypeOfRoom,BruttoPrice,Vat,Amount,Days)
select p.Id,'POKÓJ 1-OSOBOWY',242.5,8,3,3  from Proposition p  where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropAccomodationDiscount ( Id_proposition, StandardPrice,Discount,DoubleRoomEP,BussinesSingleEP,ApartmentSingleEP)
select p.Id,NULL,3,NULL,NULL,NULL from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropExtraServices(Id_proposition,ServiceType,BruttoPrice,Vat,Days,Amount)
select  p.Id,'PARKING ( część dozorowana )',3.5,8,3,3  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropExtraServices(Id_proposition,ServiceType,BruttoPrice,Vat,Days,Amount)
select  p.Id,'Inne',5,8,3,3  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropPaymentSuggestions (Id_proposition,PaymentForm,IndividualOrders,InvoiceServiceName,CarPark,HallDescription)
select   p.Id,'100% do dnia realizacji','Doliczany do faktury','Gastronomia konferencyjna 8% ; Gastronomia konferencyjna 23% ; Wynajem Sali 23% ; Usługa noclegowa 8%','Płatny indywidualnie',null from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';

--*************************************************************
select @iduser =  id from Users where Login ='miwi';
PRINT @iduser 
insert into Proposition (Id_user, UpdateDate,Status)
values (@iduser,CONVERT(VARCHAR(19),GETDATE() - 45),'W trakcie realizacji');
insert into PropClient (Id_proposition,CompanyName,CompanyAdress,NIP,CustomerFullName,PhoneNum,DecisingPersonFullName, CustomerEmail)
select  p.Id,'ZOO','ul. Wrocławska 3/2 53-783 Gdańsk','9875656698','Michał Frączewski', 852456951,'Michał Frączewski' ,'komada997@wp.pl'   from Proposition p  where p.Id_user = @iduser  and p.Status='W trakcie realizacji';
insert into PropReservationDetails (Id_proposition,StartData,StartTime,EndData,EndTime,PeopleNumber,Hall,HallSetting)
select  p.Id,CONVERT(VARCHAR(19),2015-12-29), '10:15:00' ,CONVERT(VARCHAR(19),2015-12-29), '14:15:00',6,'A',NULL from Proposition p  where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropHallEquipment(Id_proposition,Things,BruttoPrice,Vat,Amount,Days)
select  p.Id,'Sala A',285,23,3,4  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropHallEquipmentDiscount(Id_proposition,StandardPrice,Discount) 
select p.Id,300,5 from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuPosition(Id_proposition,TypeOfService,BruttoPrice,Vat,Amount,Days)
select  p.Id,'Lunch serwowany - 2 dania z przystawką',63.1401443,8,2,3  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';

insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża gastronomia 8%)',77,'MGA8' from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select p.Id,'MMarża gastronomia 23%)',78,'MGA23'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża Alkohole)',78,'MALK'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Marża niskobudżetowe)',58,'MNIS'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropMenuMerge(Id_proposition,MergeName,DefaultValue,MergeType)
select  p.Id,'Napoje)',48,'MNAPO'  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';

insert into PropAccomodation (Id_proposition,TypeOfRoom,BruttoPrice,Vat,Amount,Days)
select p.Id,'POKÓJ 1-OSOBOWY',242.5,8,3,3  from Proposition p  where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropAccomodationDiscount ( Id_proposition, StandardPrice,Discount,DoubleRoomEP,BussinesSingleEP,ApartmentSingleEP)
select p.Id,NULL,3,NULL,NULL,NULL from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropExtraServices(Id_proposition,ServiceType,BruttoPrice,Vat,Days,Amount)
select  p.Id,'PARKING ( część dozorowana )',3.5,8,3,3  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropExtraServices(Id_proposition,ServiceType,BruttoPrice,Vat,Days,Amount)
select  p.Id,'Inne',5,8,3,3  from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
insert into PropPaymentSuggestions (Id_proposition,PaymentForm,IndividualOrders,InvoiceServiceName,CarPark,HallDescription)
select   p.Id,'100% do dnia realizacji','Doliczany do faktury','Gastronomia konferencyjna 8% ; Gastronomia konferencyjna 23% ; Wynajem Sali 23% ; Usługa noclegowa 8%','Płatny indywidualnie',null from Proposition p where p.Id_user = @iduser and p.Status='W trakcie realizacji';
end
