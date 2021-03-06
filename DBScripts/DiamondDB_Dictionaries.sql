use [DiamondDB]
insert PropPaymentSuggestions_Dictionary_First (PaymentForm) values ('50 % zadatek / 50% do dnia realizacji');
insert PropPaymentSuggestions_Dictionary_First (PaymentForm) values ('100% zadatek');
insert PropPaymentSuggestions_Dictionary_First (PaymentForm) values ('100% do dnia realizacji');
insert PropPaymentSuggestions_Dictionary_First (PaymentForm) values ('Przelew 7 dni');
insert PropPaymentSuggestions_Dictionary_First (PaymentForm) values ('Przelew 14 dni');
insert PropPaymentSuggestions_Dictionary_First (PaymentForm) values ('Przelew 21 dni');
insert PropPaymentSuggestions_Dictionary_First (PaymentForm) values ('Inna');
insert PropPaymentSuggestions_Dictionary_First (PaymentForm) values ('Got�wka/karta');
insert PropPaymentSuggestions_Dictionary_First (PaymentForm) values ('Do ustalenia');

insert PropPaymentSuggestions_Dictionary_Second (InvoiceServiceName) values ('Gastronomia konferencyjna 8% ; Gastronomia konferencyjna 23% ; Wynajem Sali 23% ; Us�uga noclegowa 8%');
insert PropPaymentSuggestions_Dictionary_Second (InvoiceServiceName) values ('Gastronomia konferencyjna 8% ; Gastronomia konferencyjna 23%');
insert PropPaymentSuggestions_Dictionary_Second (InvoiceServiceName) values ('Posi�ki grupowe HP');
insert PropPaymentSuggestions_Dictionary_Second (InvoiceServiceName) values ('Posi�ki grupowe HP ; Gastronomia konferencyjna 8% ; Gastronomia konferencyjna 23%');
insert PropPaymentSuggestions_Dictionary_Second (InvoiceServiceName) values ('Us�uga Konferencyjna 23%');
insert PropPaymentSuggestions_Dictionary_Second (InvoiceServiceName) values ('Gastronomia konferencyjna 8% ; Gastronomia konferencyjna 23% ; Wynajem Sali 23%');

insert PropPaymentSuggestions_Dictionary_Third (IndividualOrders) values ('P�atny indywidualnie');
insert PropPaymentSuggestions_Dictionary_Third (IndividualOrders) values ('Rycza�t');
insert PropPaymentSuggestions_Dictionary_Third (IndividualOrders) values ('Doliczany do faktury');
insert PropPaymentSuggestions_Dictionary_Third (IndividualOrders) values ('Do ustalenia');
insert PropPaymentSuggestions_Dictionary_Third (IndividualOrders) values ('Zam�wienia trenera i reprezentat�w doliczane do faktury, go�cie p�ac� indywidualnie');

insert PropPaymentSuggestions_Dictionary_Fourth (CarPark) values ('P�atny indywidualnie');
insert PropPaymentSuggestions_Dictionary_Fourth (CarPark) values ('Rycza�t');
insert PropPaymentSuggestions_Dictionary_Fourth (CarPark) values ('Doliczany do faktury');
insert PropPaymentSuggestions_Dictionary_Fourth (CarPark) values ('Do ustalenia');
insert PropPaymentSuggestions_Dictionary_Fourth (CarPark) values ('Zam�wienia trenera i reprezentat�w doliczane do faktury, go�cie p�ac� indywidualnie');

insert PropAccomodation_Dictionary (TypeOfRoom,Price) values ('POK�J 1-OSOBOWY',250);
insert PropAccomodation_Dictionary (TypeOfRoom,Price) values ('POK�J 2-OSOBOWY',350);
insert PropAccomodation_Dictionary (TypeOfRoom,Price) values ('POK�J BUSSINES 1-OSOBOWY',400);
insert PropAccomodation_Dictionary (TypeOfRoom,Price) values ('POK�J BUSSINES 2-OSOBOWY',500);
insert PropAccomodation_Dictionary (TypeOfRoom,Price) values ('APARTAMENT',650);
insert PropAccomodation_Dictionary (TypeOfRoom,Price) values ('POKOJ DLA NIEPE�NOSPRAWNYCH',250);

insert PropHallEquipmnet_Dictionary_First (Things) values ('SALA KONFERENCYJNA');
insert PropHallEquipmnet_Dictionary_First (Things) values ('SALA KONFERENCYJNA 57m2');
insert PropHallEquipmnet_Dictionary_First (Things) values ('SALA KONFERENCYJNA 75m2');
insert PropHallEquipmnet_Dictionary_First (Things) values ('SALA KONFERENCYJNA 132m2');
insert PropHallEquipmnet_Dictionary_First (Things) values ('SALA KONFERENCYJNA 58m2');
insert PropHallEquipmnet_Dictionary_First (Things) values ('SALA KONFERENCYJNA 88m2');
insert PropHallEquipmnet_Dictionary_First (Things) values ('SALA KONFERENCYJNA 147m2');
insert PropHallEquipmnet_Dictionary_First (Things) values ('SALA KONFERENCYJNA 116m2');
insert PropHallEquipmnet_Dictionary_First (Things) values ('SALA KONFERENCYJNA 163m2');
insert PropHallEquipmnet_Dictionary_First (Things) values ('SALA KONFERENCYJNA 41m2');
insert PropHallEquipmnet_Dictionary_First (Things) values ('SALA KONFERENCYJNA 175m2');
insert PropHallEquipmnet_Dictionary_First (Things) values ('SALA KONFERENCYJNA 45m2');

insert PropHallEquipmnet_Dictionary_Second (Things) values ('RZUTNIK, EKRAN, FLIPCHART');
insert PropHallEquipmnet_Dictionary_Second (Things) values ('RZUTNIK, EKRAN');
insert PropHallEquipmnet_Dictionary_Second (Things) values ('EKRAN, FLIPCHART');
insert PropHallEquipmnet_Dictionary_Second (Things) values ('FLIPCHART');
insert PropHallEquipmnet_Dictionary_Second (Things) values ('EKRAN');
insert PropHallEquipmnet_Dictionary_Second (Things) values ('RZUTNIK');
insert PropHallEquipmnet_Dictionary_Second (Things) values ('LAPTOP');
insert PropHallEquipmnet_Dictionary_Second (Things) values ('NAG�O�NIENIE');
insert PropHallEquipmnet_Dictionary_Second (Things) values ('NAG�O�NIENIE DO LAPTOPA');
insert PropHallEquipmnet_Dictionary_Second (Things) values ('KRZES�A Z PULPITAMI');

insert PropExtraServices_Dictionary (ServiceType, Price) values ('PARKING ( cz�� nie dozorowana )', 0.0);
insert PropExtraServices_Dictionary (ServiceType, Price) values ('PARKING ( cz�� dozorowana )', 3.5);

insert PropMenuMerge_Dictionary_First (MergeName,Value) values ('Mar�a gastronomia 8%)',80);
insert PropMenuMerge_Dictionary_First (MergeName,Value) values ('Mar�a gastronomia 23%)',80);
insert PropMenuMerge_Dictionary_First (MergeName,Value) values ('Mar�a Alkohole)',80);
insert PropMenuMerge_Dictionary_First (MergeName,Value) values ('Mar�a niskobud�etowe)',60);
insert PropMenuMerge_Dictionary_First (MergeName,Value) values ('Napoje)',50);

insert PropMergeTypes_Dictionary (MergeType) values ('MGA8');
insert PropMergeTypes_Dictionary (MergeType) values ('MGA23');
insert PropMergeTypes_Dictionary (MergeType) values ('MALK');
insert PropMergeTypes_Dictionary (MergeType) values ('MNIS');
insert PropMergeTypes_Dictionary (MergeType) values ('MNAPO');


insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch serwowany - 3 dania', 38.12,0.08,'MGA8' ,'Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch serwowany - 2 dania z zup�', 31.77 ,0.08,'MGA8','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch serwowany - 2 dania z przystawk�', 33.03 ,0.08,'MGA8','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch serwowany - 1 danie g��wne', 25.42 ,0.08,'MGA8','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Zestaw �l�ski ( zupa + danie g��wne + deser,SpecificType)', 45.39 ,0.08,'MGA8','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch serwowany - Menu I Sl�skie smaki', 35.66 ,0.08,'MGA8','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch serwowany - Menu II Tradycyjny obiad', 37.82 ,0.08,'MGA8','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch serwowany - Menu III Wspomnienia lata', 38.90 ,0.08,'MGA8','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch serwowany - Menu IV Jak u mamy', 39.98 ,0.08,'MGA8','Lunch');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch bufetowy (2-zupy , 2-dodatki mi�sne 1 dodatek rybny, 2-dodatki syc�ce, 3-sur�wki, 2 desery)',  64.19 ,0.08,'MGA8','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch bufetowy Menu I ', 46.46 ,0.08,'MGA8','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch bufetowy Menu II ', 47.54 ,0.08,'MGA8','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch bufetowy Menu III ', 49.71 ,0.08,'MGA8','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch bufetowy Menu I, II, III powyzej 100os.',38.90  ,0.08,'MGA8','Lunch');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bufet NAPOJE (kawa, herbata, woda, sok)', 8.92 ,0.23,'MGA23','Bufet');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bufet NAPOJE (serwowany 2 krotnie) (kawa, herbata, woda, sok)', 13.38 ,0.23,'MGA23','Bufet');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bufet NAPOJE (serwowany ci�gle) (kawa, herbata, woda, sok)', 17.84 ,0.23,'MGA23','Bufet');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bufet II standard', 12.59 , 0.23,'MGA23','Bufet');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bufet II standard UZUPE�NIANY', 17.08 ,0.23,'MGA23','Bufet');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bufet II standard CI�G�Y', 25.62 ,0.23,'MGA23','Bufet');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bufet III standard plus', 15.18, 0.23,'MGA23','Bufet');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bufet III standard plus UZUPE�NIANY', 24.67 ,0.23,'MGA23','Bufet');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bufet III standard plus CI�G�Y', 35.10 , 0.23,'MGA23','Bufet');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bufet IV energetyzuj�cy', 18.98 , 0.23,'MGA23','Bufet');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bufet IV energetyzuj�cy UZUPE�NIANY', 35.10 ,0.23,'MGA23','Bufet');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bufet IV energetyzuj�cy CI�G�Y', 45.54, 0.23,'MGA23','Bufet');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Sa�atka owocowa 100g/os', 4.28, 0.08,'MGA23','Bufet');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Owoce 100g/os', 3.31 ,0.08,'MGA23','Bufet');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Kanapki dekoracyjne 3szt/os',7.81, 0.08,'MGA23','Bufet');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Ciasto domowe 2 kawa�ki/os', 6.62,0.08,'MGA23','Bufet');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Kolacja "Serwowana"', 39.38 ,0.08,'MGA8','Kolacja');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Kolacja serwowana menu I', 34.16 ,0.08,'MGA8','Kolacja');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Kolacja serwowana menu II' ,40.80 ,0.08,'MGA8','Kolacja');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Kolacja "Bufet"' , 39.38 ,0.08,'MGA8','Kolacja');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Kolacja bufetowa menu I', 34.16 ,0.08,'MGA8','Kolacja');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Kolacja bufetowa menu II', 36.05 ,0.08,'MGA8','Kolacja');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Grill z Grillo-w�dzarni�', 41.94 ,0.08,'MGA8','Kolacja');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Grill Menu II', 47.02 ,0.08,'MGA8','Grill');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Grill Menu III', 53.38 ,0.08,'MGA8','Grill');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Kolacja Bufetowa + zimna p�yta', 85.31 ,0.08,'MGA8','Kolacja');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Zimna p�yta "Bufet"', 34.48 ,0.08,'MGA8', 'Bufet' );

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bufet przek�sek zimnych + gor�ca kolacja', 30.63 ,0.08,'MGA8', 'Bufet');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bufet przek�sek zestaw II', 30.63 ,0.08,'MGA8', 'Bufet');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bufet przek�sek zestaw III', 30.63 ,0.08,'MGA8', 'Bufet');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bufet przek�sek zestaw IV', 34.31 ,0.08,'MGA8', 'Bufet');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Barszcz �czerwony z korokietem ', 14.70 ,0.08,'MGA8','Dania');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Zupa curry z kurczaka' , 14.70 ,0.08,'MGA8','Dania');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Zupa gulaszowa� pikantna� w�gierska' , 14.70 ,0.08,'MGA8','Dania');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Curry z kurczaka z bia�ym ryzem' , 24.51 ,0.08,'MGA8','Dania');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Strogonof z wo�owiny z pieczywem' , 14.70 ,0.08,'MGA8','Dania');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Szasyki drobiowe z frytkami' , 24.51 ,0.08,'MGA8','Dania');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('�ywiec 0.3 l', 4.67  ,0.23,'MALK','Piwo');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('�ywiec 0.5 l', 6.42  ,0.23,'MALK','Piwo');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('�ywiec 30 l', 350.10  ,0.23,'MALK','Piwo');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('�ywiec bia�e 0.5 l', 5.84  ,0.23,'MALK','Piwo');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('�ywiec bezalkohoolowy 0.33 l', 4.08  ,0.23,'MALK','Piwo');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Wyborowa 0,04 l', 3.50,0.23,'MALK','W�dki');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Wyborowa 0,5 l', 35.01,0.23,'MALK','W�dki');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Finlandia 0,04 l', 4.67,0.23,'MALK','W�dki');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Finlandia 0,5 l', 45.51,0.23,'MALK','W�dki');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Absolut 0,04 l', 4.67,0.23,'MALK','W�dki');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Absolut 0,5 l', 45.51,0.23,'MALK','W�dki');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('�ubr�wka 0,04 l', 3.50,0.23,'MALK','W�dki');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('�ubr�wka 0,5 l', 35.01,0.23,'MALK','W�dki');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Wi�ni�wka 0,04 l', 3.50,0.23,'MALK','W�dki');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Wi�ni�wka 0,5 l', 35.01,0.23,'MALK','W�dki');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Gorzka �o��dkowa 0,04 l', 3.50,0.23,'MALK','W�dki');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Gorzka �o��dkowa0,5 l', 35.01,0.23,'MALK','W�dki');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Ballantine''s Finest 0,04 l', 5.84,0.23,'MALK','Whisky');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Ballantine''s Finest 0,7 l', 102.11,0.23,'MALK','Whisky');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Ballantine''s 12 years 0,04 l', 8.17,0.23,'MALK','Whisky');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Ballantine''s 12 years 0,7 l', 142.96,0.23,'MALK','Whisky');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Johnie Walker Red Label 0,04 l', 5.25,0.23,'MALK','Whisky');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Johnie Walker Red Label 0,7 l', 91.90,0.23,'MALK','Whisky');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Johnie Walker Black Label 0,04 l', 9.34,0.23,'MALK','Whisky');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Johnie Walker Black Label 0,7 l', 163.38,0.23,'MALK','Whisky');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Chivas Regal 0,04 l', 8.17,0.23,'MALK','Whisky');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Chivas Regal 0,7 l', 142.96,0.23,'MALK','Whisky');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Jameson 0,04 l', 5.84,0.23,'MALK','Whisky');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Jameson 0,7 l', 102.11,0.23,'MALK','Whisky');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Jack Daniels 0,04 l', 7.00,0.23,'MALK','Whisky');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Jack Daniels 0,7 l', 122.54,0.23,'MALK','Whisky');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Campari 0,04 l', 5.25,0.23,'MALK','Whisky');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Campari 0,7 l', 91.90,0.23,'MALK','Whisky');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Martini Bianco 0,1 l', 5.25,0.23,'MALK','Whisky');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Martini Bianco 1 l', 52.52,0.23,'MALK','Whisky');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Martini Extra Dry 0,1 l', 5.25,0.23,'MALK','Whisky');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Martini Extra Dry 1l', 52.52,0.23,'MALK','Whisky');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Martini Rosso 0,1 l', 5.25,0.23,'MALK','Whisky');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Martini Rosso 1 l', 52.52,0.23,'MALK','Whisky');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bacardi Superior 0,04 l', 7.59,0.23,'MALK','Rum');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bacardi Superior 0,7 l', 132.75,0.23,'MALK','Rum');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bacardi Black 0,04 l', 6.42,0.23,'MALK','Rum');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bacardi Black 0,7 l', 112.32,0.23,'MALK','Rum');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Havana Club 3 0,04 l', 6.42,0.23,'MALK','Rum');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Havana Club 3 0,7 l', 112.32,0.23,'MALK','Rum');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Havana Club 7 0,04 l', 7.59,0.23,'MALK','Rum');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Havana Club 7 0,7 l', 132.75,0.23,'MALK','Rum');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bailey''s Irish Cream 0,04 l', 5.25,0.23,'MALK','Likier');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bailey''s Irish Cream 0,7 l', 91.90,0.23,'MALK','Likier');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Jagermeister 0,04 l', 5.25,0.23,'MALK','Likier');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Jagermeister 0,7 l', 91.90,0.23,'MALK','Likier');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Malibu 0,04 l', 5.25,0.23,'MALK','Likier');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Malibu 0,7 l', 91.90,0.23,'MALK','Likier');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Passoa 0,04 l', 5.25,0.23,'MALK','Likier');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Passoa 0,7 l', 91.90,0.23,'MALK','Likier');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Seagrams 0,04 l', 4.67,0.23,'MALK','Gin');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Seagrams 0,7 l', 81.69,0.23,'MALK','Gin');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Metaxa ***** 0,04 l', 5.84,0.23,'MALK','Gin');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Metaxa ***** 0,7 l', 102.11,0.23,'MALK','Gin');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Martel VS 0,04 l', 9.34,0.23,'MALK','Gin');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Martel VS 0,7 l', 163.38,0.23,'MALK','Gin');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Tequila Silver 0,04 l', 5.84,0.23,'MALK','Tequila');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Tequila Silver 0,7 l', 102.11,0.23,'MALK','Tequila');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Tequila Gold 0,04 l', 6.42,0.23,'MALK','Tequila');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Tequila Gold 0,7 l', 112.32,0.23,'MALK','Tequila');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('SantaCarolina Premio 0,15l C', 4.67,0.23,'MALK','Wina C');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('SantaCarolina Premio 0,75l C', 29.18,0.23,'MALK','Wina C');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Elilaio Trebbiano D''Abruzzo 0,15l C', 5.84,0.23,'MALK','Wina C');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Elilaio Trebbiano D''Abruzzo 0,75l C', 35.01,0.23,'MALK','Wina C');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Kumala Shiraz Pinotage 0,75l C', 46.68,0.23,'MALK','Wina C');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Vina Bujanda Tempranillo 0,75l C', 52.52,0.23,'MALK','Wina C');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Chateau Pey La Tour 0,75l C', 58.35,0.23,'MALK','Wina C');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Peppoli Chianti Classico 0,75l C', 116.70,0.23,'MALK','Wina C');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Santa Carolina Premio 0,15l B', 4.67,0.23,'MALK','Wina B');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Santa Carolina Premio 0,75l B', 29.18,0.23,'MALK','Wina B');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Elilaio Trebbiano D''Abruzzo 0,15l B', 5.84,0.23,'MALK','Wina B');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Elilaio Trebbiano D''Abruzzo 0,75l B', 35.01,0.23,'MALK','Wina B');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Kumala Semillon Chardonnay 0,75l B', 46.68,0.23,'MALK','Wina B');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Bordeaus 0,75l B', 46.68,0.23,'MALK','Wina B');
																			
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Cava Rigol Brut 0,1l', 4.67,0.23,'MALK','Szampany');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Cava Rigol Brut 0,75l', 46.68,0.23,'MALK','Szampany');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Jean De La Fontaine Brut 0,75l', 116.70,0.23,'MALK','Szampany');
																			
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Pepsi 0.2l', 4.08, 23,'MNAPO','Napoje');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('PepsiMax 0.2l', 4.08,23,'MNAPO','Napoje');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Pepsi Light 0.2l', 4.08 ,23,'MNAPO','Napoje');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Mirinda 0.2l', 4.08 ,23,'MNAPO','Napoje');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('7 UP 0.2l', 4.08 ,23,'MNAPO','Napoje');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Schwepps 0.2l', 4.08 ,23,'MNAPO','Napoje');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('G�rska natura gazowana 0.3l', 4.08 ,23,'MNAPO','Napoje');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('G�rska natura niegazowana 0.3l', 4.08 ,23,'MNAPO','Napoje');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Red bull 0.2l', 5.84 ,23,'MNAPO','Napoje');
																			
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('�niadania grupowe niskobud�etowe (ekonomiczne)', 22.69 ,0.08,'MNIS','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch serwowany - Menu I', 23.77 ,0.08,'MNIS','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch serwowany - Menu II', 25.93 ,0.08 ,'MNIS','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch serwowany - Menu III', 27.01 ,0.08 ,'MNIS','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch serwowany - Menu IV', 28.09 ,0.08 ,'MNIS','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch serwowany - Menu V', 29.18 ,0.08 ,'MNIS','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch bufetowy - Menu I', 25.93 ,0.08 ,'MNIS','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch bufetowy - Menu II', 27.01 ,0.08 ,'MNIS','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch bufetowy - Menu III', 28.09 ,0.08,'MNIS','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch bufetowy - Menu IV', 29.18 ,0.08,'MNIS','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Lunch bufetowy - Menu V', 30.26 ,0.08,'MNIS','Lunch');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Kolacja bufetowa - Menu I', 22.69 ,0.08,'MNIS','Kolacja');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Kolacja bufetowa - Menu II', 23.77 ,0.08 ,'MNIS','Kolacja');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Kolacja bufetowa - Menu III', 25.93 ,0.08 ,'MNIS','Kolacja');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('Kolacja bufetowa - Menu IV', 28.09 ,0.08 ,'MNIS','Kolacja');
																			
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('OpenBar: STANDARD do 4h', 56.02 ,0.23,'MALK','OpenBar');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('OpenBar: STANDARD od 4h do 8h', 84.02 ,0.23,'MALK','OpenBar');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('OpenBar: STANDARD powy�ej 8h', 112.03 ,0.23 ,'MALK','OpenBar');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('OpenBar: PREMIUM STANDARD do 4h', 67.69 ,0.23,'MALK','OpenBar');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('OpenBar: PREMIUM od 4h do 8h', 101.53 ,0.23,'MALK','OpenBar');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('OpenBar: PREMIUM powy�ej 8h', 135.37 ,0.23 ,'MALK','OpenBar');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('OpenBar: DIAMENT do 4h', 79.36 ,0.23 ,'MALK','OpenBar');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('OpenBar: DIAMENT od 4h do 8h', 119.03 ,0.23 ,'MALK','OpenBar');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType,SpecificType) values ('OpenBar: DIAMENT powy�ej 8h', 158.71 ,0.23 ,'MALK','OpenBar');

insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,February,March,April,May,June,July,August,September,October,November,December,Other) values ('A',400,400,600,400,600,300,300,400,600,600,400,300,1800);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,February,March,April,May,June,July,August,September,October,November,December,Other) values ('B',500,500,800,600,800,375,375,500,800,800,600,375,2400);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,February,March,April,May,June,July,August,September,October,November,December,Other) values ('A+B',900,900,1500,1000,1500,675,675,900,1500,1500,1000,675,4500);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,February,March,April,May,June,July,August,September,October,November,December,Other) values ('C',400,400,700,500,700,300,300,400,700,700,500,300,2100);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,February,March,April,May,June,July,August,September,October,November,December,Other) values ('D',600,600,1000,800,1000,450,450,600,1000,1000,800,450,3000);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,February,March,April,May,June,July,August,September,October,November,December,Other) values ('C+D',1000,1000,1600,1300,1600,750,750,1000,1600,1600,1300,750,4800);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,February,March,April,May,June,July,August,September,October,November,December,Other) values ('A+C',800,800,1000,1000,1000,600,600,800,1000,1000,1000,600,3000);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,February,March,April,May,June,July,August,September,October,November,December,Other) values ('B+D',1100,1100,1300,1400,1300,825,825,1100,1300,1300,1400,825,3900);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,February,March,April,May,June,July,August,September,October,November,December,Other) values ('DIAMENT',2500,2500,4000,3000,4000,1750,1750,2500,4000,4000,3000,1750,12000);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,February,March,April,May,June,July,August,September,October,November,December,Other) values ('KORAL',300,300,600,400,600,250,250,300,600,600,400,250,1800);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,February,March,April,May,June,July,August,September,October,November,December,Other) values ('Atmosfera',1300,1300,1900,1500,1900,900,900,1300,1900,1900,1500,900,5700);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,February,March,April,May,June,July,August,September,October,November,December,Other) values ('VIP',400,400,700,500,700,300,300,400,700,700,500,300,2100);

insert into PropReservationDetails_Dictionary_HallCapacity (Hall,Area,TheatrePeopleNumber,UShapePeopleNumber) values ('A',57,44,30)
insert into PropReservationDetails_Dictionary_HallCapacity (Hall,Area,TheatrePeopleNumber,UShapePeopleNumber) values ('B',75,100,30)
insert into PropReservationDetails_Dictionary_HallCapacity (Hall,Area,TheatrePeopleNumber,UShapePeopleNumber) values ('A+B',132,142,60)
insert into PropReservationDetails_Dictionary_HallCapacity (Hall,Area,TheatrePeopleNumber,UShapePeopleNumber) values ('C',58,44,30)
insert into PropReservationDetails_Dictionary_HallCapacity (Hall,Area,TheatrePeopleNumber,UShapePeopleNumber) values ('D',88,100,30)
insert into PropReservationDetails_Dictionary_HallCapacity (Hall,Area,TheatrePeopleNumber,UShapePeopleNumber) values ('C+D',147,142,60)
insert into PropReservationDetails_Dictionary_HallCapacity (Hall,Area,TheatrePeopleNumber,UShapePeopleNumber) values ('A+C',116,90,60)
insert into PropReservationDetails_Dictionary_HallCapacity (Hall,Area,TheatrePeopleNumber,UShapePeopleNumber) values ('B+D',163,200,60)
insert into PropReservationDetails_Dictionary_HallCapacity (Hall,Area,TheatrePeopleNumber,UShapePeopleNumber) values ('DIAMENT',390,350,150)
insert into PropReservationDetails_Dictionary_HallCapacity (Hall,Area,TheatrePeopleNumber,UShapePeopleNumber) values ('KORAL',41,30,20)
insert into PropReservationDetails_Dictionary_HallCapacity (Hall,Area,TheatrePeopleNumber,UShapePeopleNumber) values ('Atmosfera',175,100,80)
insert into PropReservationDetails_Dictionary_HallCapacity (Hall,Area,TheatrePeopleNumber,UShapePeopleNumber) values ('VIP',45,30,20)

insert into [PropReservationDetails_Dictionary_HallSettings] (Setting) values ('Podkowa - U')
insert into [PropReservationDetails_Dictionary_HallSettings] (Setting) values ('Teatr')
insert into [PropReservationDetails_Dictionary_HallSettings] (Setting) values ('Szkolne')
insert into [PropReservationDetails_Dictionary_HallSettings] (Setting) values ('Prezentacja')
insert into [PropReservationDetails_Dictionary_HallSettings] (Setting) values ('Inne')
insert into [PropReservationDetails_Dictionary_HallSettings] (Setting) values ('Do ustalenia')
insert into [PropReservationDetails_Dictionary_HallSettings] (Setting) values ('Bankiet')
insert into [PropReservationDetails_Dictionary_HallSettings] (Setting) values ('Wsp�lny st�')
insert into [PropReservationDetails_Dictionary_HallSettings] (Setting) values ('Teatr / Bankiet')

insert into [VatList] (Vat) values ('8');
insert into [VatList] (Vat) values ('23');

update PropMenuGastronomicThings_Dictionary_First set Vat = '23' where MergeType = 'MALK'
update PropMenuGastronomicThings_Dictionary_First set Vat = '23' where MergeType = 'MGA23'
update PropMenuGastronomicThings_Dictionary_First set Vat = '8' where MergeType = 'MNIS'
update PropMenuGastronomicThings_Dictionary_First set Vat = '8' where MergeType = 'MGA8'


insert PropExtraServices_Dictionary (ServiceType,Price) values (' ',null);
insert into PropPaymentSuggestions_Dictionary_First values (' ')
insert into PropPaymentSuggestions_Dictionary_Second values (' ')
insert into PropPaymentSuggestions_Dictionary_Third values (' ')
insert into PropPaymentSuggestions_Dictionary_Fourth values (' ')
insert into PropReservationDetails_Dictionary_HallCapacity (Hall,Area,TheatrePeopleNumber,UshapePeopleNumber) values (' ',null,null,null)
insert into PropReservationDetails_Dictionary_HallSettings values (' ')
insert into PropHallEquipmnet_Dictionary_Second values (' ')

insert into PropositionStates_Dictionary values ('Nowa')
insert into PropositionStates_Dictionary values ('W trakcie realizacji')
insert into PropositionStates_Dictionary values ('Zrealizowana')