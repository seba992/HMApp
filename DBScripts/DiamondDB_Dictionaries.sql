insert PropPaymentSuggestions_Dictionary_First (PaymentForm) values ('50 % zadatek / 50% do dnia realizacji');
insert PropPaymentSuggestions_Dictionary_First (PaymentForm) values ('100% zadatek');
insert PropPaymentSuggestions_Dictionary_First (PaymentForm) values ('100% do dnia realizacji');
insert PropPaymentSuggestions_Dictionary_First (PaymentForm) values ('Przelew 7 dni');
insert PropPaymentSuggestions_Dictionary_First (PaymentForm) values ('Przelew 14 dni');
insert PropPaymentSuggestions_Dictionary_First (PaymentForm) values ('Przelew 21 dni');
insert PropPaymentSuggestions_Dictionary_First (PaymentForm) values ('Inna');
insert PropPaymentSuggestions_Dictionary_First (PaymentForm) values ('Gotówka/karta');
insert PropPaymentSuggestions_Dictionary_First (PaymentForm) values ('Do ustalenia');

insert PropPaymentSuggestions_Dictionary_Second (InvoiceServiceName) values ('Gastronomia konferencyjna 8% ; Gastronomia konferencyjna 23% ; Wynajem Sali 23% ; Us³uga noclegowa 8%');
insert PropPaymentSuggestions_Dictionary_Second (InvoiceServiceName) values ('Gastronomia konferencyjna 8% ; Gastronomia konferencyjna 23%');
insert PropPaymentSuggestions_Dictionary_Second (InvoiceServiceName) values ('Posi³ki grupowe HP');
insert PropPaymentSuggestions_Dictionary_Second (InvoiceServiceName) values ('Posi³ki grupowe HP ; Gastronomia konferencyjna 8% ; Gastronomia konferencyjna 23%');
insert PropPaymentSuggestions_Dictionary_Second (InvoiceServiceName) values ('Us³uga Konferencyjna 23%');
insert PropPaymentSuggestions_Dictionary_Second (InvoiceServiceName) values ('Gastronomia konferencyjna 8% ; Gastronomia konferencyjna 23% ; Wynajem Sali 23%');

insert PropPaymentSuggestions_Dictionary_Third (IndividualOrders) values ('P³atny indywidualnie');
insert PropPaymentSuggestions_Dictionary_Third (IndividualOrders) values ('Rycza³t');
insert PropPaymentSuggestions_Dictionary_Third (IndividualOrders) values ('Doliczany do faktury');
insert PropPaymentSuggestions_Dictionary_Third (IndividualOrders) values ('Do ustalenia');
insert PropPaymentSuggestions_Dictionary_Third (IndividualOrders) values ('Zamówienia trenera i reprezentatów doliczane do faktury, goœcie p³ac¹ indywidualnie');

insert PropPaymentSuggestions_Dictionary_Fourth (CarPark) values ('P³atny indywidualnie');
insert PropPaymentSuggestions_Dictionary_Fourth (CarPark) values ('Rycza³t');
insert PropPaymentSuggestions_Dictionary_Fourth (CarPark) values ('Doliczany do faktury');
insert PropPaymentSuggestions_Dictionary_Fourth (CarPark) values ('Do ustalenia');
insert PropPaymentSuggestions_Dictionary_Fourth (CarPark) values ('Zamówienia trenera i reprezentatów doliczane do faktury, goœcie p³ac¹ indywidualnie');

insert PropAccomodation_Dictionary (TypeOfRoom) values ('POKÓJ 1-OSOBOWY');
insert PropAccomodation_Dictionary (TypeOfRoom) values ('POKÓJ 2-OSOBOWY');
insert PropAccomodation_Dictionary (TypeOfRoom) values ('POKÓJ BUSSINES 1-OSOBOWY');
insert PropAccomodation_Dictionary (TypeOfRoom) values ('POKÓJ BUSSINES 2-OSOBOWY');
insert PropAccomodation_Dictionary (TypeOfRoom) values ('APARTAMENT');
insert PropAccomodation_Dictionary (TypeOfRoom) values ('JUNIOR');
insert PropAccomodation_Dictionary (TypeOfRoom) values ('POKOJ DLA NIEPE£NOSPRAWNYCH');
insert PropAccomodation_Dictionary (TypeOfRoom) values ('BAGA¯OWNIA');

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
insert PropHallEquipmnet_Dictionary_Second (Things) values ('NAG£OŒNIENIE');
insert PropHallEquipmnet_Dictionary_Second (Things) values ('NAG£OŒNIENIE DO LAPTOPA');
insert PropHallEquipmnet_Dictionary_Second (Things) values ('KRZES£A Z PULPITAMI');

insert PropExtraServices_Dictionary (ServiceType) values ('PARKING ( czêœæ nie dozorowana )');
insert PropExtraServices_Dictionary (ServiceType) values ('PARKING ( czêœæ dozorowana )');

insert PropMenuMerge_Dictionary_First (MergeName,Value) values ('Mar¿a gastronomia 8%)',80);
insert PropMenuMerge_Dictionary_First (MergeName,Value) values ('Mar¿a gastronomia 23%)',80);
insert PropMenuMerge_Dictionary_First (MergeName,Value) values ('Mar¿a Alkohole)',80);
insert PropMenuMerge_Dictionary_First (MergeName,Value) values ('Mar¿a niskobud¿etowe)',60);
insert PropMenuMerge_Dictionary_First (MergeName,Value) values ('Napoje)',50);

insert PropMergeTypes_Dictionary (MergeType) values ('MGA8');
insert PropMergeTypes_Dictionary (MergeType) values ('MGA23');
insert PropMergeTypes_Dictionary (MergeType) values ('MALK');
insert PropMergeTypes_Dictionary (MergeType) values ('MNIS');
insert PropMergeTypes_Dictionary (MergeType) values ('MNAPO');


insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch serwowany - 3 dania', 38.12,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch serwowany - 2 dania z zup¹', 31.77 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch serwowany - 2 dania z przystawk¹', 33.03 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch serwowany - 1 danie g³ówne', 25.42 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Zestaw œl¹ski ( zupa + danie g³ówne + deser)', 45.39 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch serwowany - Menu I Sl¹skie smaki', 35.66 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch serwowany - Menu II Tradycyjny obiad', 37.82 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch serwowany - Menu III Wspomnienia lata', 38.90 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch serwowany - Menu IV Jak u mamy', 39.98 ,0.08,'MGA8');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch bufetowy (2-zupy , 2-dodatki miêsne 1 dodatek rybny, 2-dodatki syc¹ce, 3-surówki, 2 desery)',  64.19 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch bufetowy Menu I ', 46.46 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch bufetowy Menu II ', 47.54 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch bufetowy Menu III ', 49.71 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch bufetowy Menu I, II, III powyzej 100os.',38.90  ,0.08,'MGA8');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bufet NAPOJE (kawa, herbata, woda, sok)', 8.92 ,0.23,'MGA23');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bufet NAPOJE (serwowany 2 krotnie) (kawa, herbata, woda, sok)', 13.38 ,0.23,'MGA23');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bufet NAPOJE (serwowany ci¹gle) (kawa, herbata, woda, sok)', 17.84 ,0.23,'MGA23');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bufet II standard', 12.59 , 0.23,'MGA23');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bufet II standard UZUPE£NIANY', 17.08 ,0.23,'MGA23');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bufet II standard CI¥G£Y', 25.62 ,0.23,'MGA23');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bufet III standard plus', 15.18, 0.23,'MGA23');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bufet III standard plus UZUPE£NIANY', 24.67 ,0.23,'MGA23');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bufet III standard plus CI¥G£Y', 35.10 , 0.23,'MGA23');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bufet IV energetyzuj¹cy', 18.98 , 0.23,'MGA23');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bufet IV energetyzuj¹cy UZUPE£NIANY', 35.10 ,0.23,'MGA23');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bufet IV energetyzuj¹cy CI¥G£Y', 45.54, 0.23,'MGA23');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Sa³atka owocowa 100g/os', 4.28, 0.08,'MGA23');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Owoce 100g/os', 3.31 ,0.08,'MGA23');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Kanapki dekoracyjne 3szt/os',7.81, 0.08,'MGA23');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Ciasto domowe 2 kawa³ki/os', 6.62,0.08,'MGA23');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Kolacja "Serwowana"', 39.38 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Kolacja serwowana menu I', 34.16 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Kolacja serwowana menu II' ,40.80 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Kolacja "Bufet"' , 39.38 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Kolacja bufetowa menu I', 34.16 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Kolacja bufetowa menu II', 36.05 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Grill z Grillo-wêdzarni¹', 41.94 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Grill Menu II', 47.02 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Grill Menu III', 53.38 ,0.08,'MGA8');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Kolacja Bufetowa + zimna p³yta', 85.31 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Zimna p³yta "Bufet"', 34.48 ,0.08,'MGA8');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bufet przek¹sek zimnych + gor¹ca kolacja', 30.63 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bufet przek¹sek zestaw II', 30.63 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bufet przek¹sek zestaw III', 30.63 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bufet przek¹sek zestaw IV', 34.31 ,0.08,'MGA8');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Barszcz  czerwony z korokietem ', 14.70 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Zupa curry z kurczaka' , 14.70 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Zupa gulaszowa  pikantna  wêgierska' , 14.70 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Curry z kurczaka z bia³ym ryzem' , 24.51 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Strogonof z wo³owiny z pieczywem' , 14.70 ,0.08,'MGA8');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Szasyki drobiowe z frytkami' , 24.51 ,0.08,'MGA8');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('¯ywiec 0.3 l', 4.67  ,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('¯ywiec 0.5 l', 6.42  ,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('¯ywiec 30 l', 350.10  ,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('¯ywiec bia³e 0.5 l', 5.84  ,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('¯ywiec bezalkohoolowy 0.33 l', 4.08  ,0.23,'MALK');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('¯ywiec 0.3 l', 4.67  ,0.23,'MALK');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Wyborowa 0,04 l', 3.50,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Wyborowa 0,5 l', 35.01,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Finlandia 0,04 l', 4.67,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Finlandia 0,5 l', 45.51,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Absolut 0,04 l', 4.67,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Absolut 0,5 l', 45.51,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('¯ubrówka 0,04 l', 3.50,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('¯ubrówka 0,5 l', 35.01,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Wiœniówka 0,04 l', 3.50,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Wiœniówka 0,5 l', 35.01,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Gorzka ¿o³¹dkowa 0,04 l', 3.50,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Gorzka ¿o³¹dkowa0,5 l', 35.01,0.23,'MALK');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Ballantine''s Finest 0,04 l', 5.84,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Ballantine''s Finest 0,7 l', 102.11,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Ballantine''s 12 years 0,04 l', 8.17,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Ballantine''s 12 years 0,7 l', 142.96,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Johnie Walker Red Label 0,04 l', 5.25,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Johnie Walker Red Label 0,7 l', 91.90,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Johnie Walker Black Label 0,04 l', 9.34,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Johnie Walker Black Label 0,7 l', 163.38,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Chivas Regal 0,04 l', 8.17,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Chivas Regal 0,7 l', 142.96,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Jameson 0,04 l', 5.84,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Jameson 0,7 l', 102.11,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Jack Daniels 0,04 l', 7.00,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Jack Daniels 0,7 l', 122.54,0.23,'MALK');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Campari 0,04 l', 5.25,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Campari 0,7 l', 91.90,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Martini Bianco 0,1 l', 5.25,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Martini Bianco 1 l', 52.52,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Martini Extra Dry 0,1 l', 5.25,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Martini Extra Dry 1l', 52.52,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Martini Rosso 0,1 l', 5.25,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Martini Rosso 1 l', 52.52,0.23,'MALK');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bacardi Superior 0,04 l', 7.59,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bacardi Superior 0,7 l', 132.75,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bacardi Black 0,04 l', 6.42,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bacardi Black 0,7 l', 112.32,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Havana Club 3 0,04 l', 6.42,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Havana Club 3 0,7 l', 112.32,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Havana Club 7 0,04 l', 7.59,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Havana Club 7 0,7 l', 132.75,0.23,'MALK');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bailey''s Irish Cream 0,04 l', 5.25,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bailey''s Irish Cream 0,7 l', 91.90,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Jagermeister 0,04 l', 5.25,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Jagermeister 0,7 l', 91.90,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Malibu 0,04 l', 5.25,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Malibu 0,7 l', 91.90,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Passoa 0,04 l', 5.25,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Passoa 0,7 l', 91.90,0.23,'MALK');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Seagrams 0,04 l', 4.67,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Seagrams 0,7 l', 81.69,0.23,'MALK');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Metaxa ***** 0,04 l', 5.84,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Metaxa ***** 0,7 l', 102.11,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Martel VS 0,04 l', 9.34,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Martel VS 0,7 l', 163.38,0.23,'MALK');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Tequila Silver 0,04 l', 5.84,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Tequila Silver 0,7 l', 102.11,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Tequila Gold 0,04 l', 6.42,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Tequila Gold 0,7 l', 112.32,0.23,'MALK');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('SantaCarolina Premio 0,15l', 4.67,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('SantaCarolina Premio 0,75l', 29.18,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Elilaio Trebbiano D''Abruzzo 0,15l', 5.84,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Elilaio Trebbiano D''Abruzzo 0,75l', 35.01,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Kumala Shiraz Pinotage 0,75l', 46.68,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Vina Bujanda Tempranillo 0,75l', 52.52,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Chateau Pey La Tour 0,75l', 58.35,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Peppoli Chianti Classico 0,75l', 116.70,0.23,'MALK');

insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Santa Carolina Premio 0,15l', 4.67,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Santa Carolina Premio 0,75l', 29.18,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Elilaio Trebbiano D''Abruzzo 0,15l', 5.84,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Elilaio Trebbiano D''Abruzzo 0,75l', 35.01,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Kumala Semillon Chardonnay 0,75l', 46.68,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Bordeaus 0,75l', 46.68,0.23,'MALK');
																			
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Cava Rigol Brut 0,1l', 4.67,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Cava Rigol Brut 0,75l', 46.68,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Jean De La Fontaine Brut 0,75l', 116.70,0.23,'MALK');
																			
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Pepsi 0.2l', 4.08 ,0.23,'MNAPO');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('PepsiMax 0.2l', 4.08 ,0.23,'MNAPO');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Pepsi Light 0.2l', 4.08 ,0.23,'MNAPO');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Mirinda 0.2l', 4.08 ,0.23,'MNAPO');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('7 UP 0.2l', 4.08 ,0.23,'MNAPO');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Schwepps 0.2l', 4.08 ,0.23,'MNAPO');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Górska natura gazowana 0.3l', 4.08 ,0.23,'MNAPO');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Górska natura niegazowana 0.3l', 4.08 ,0.23,'MNAPO');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Red bull 0.2l', 5.84 ,0.23,'MNAPO');
																			
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Œniadania grupowe niskobud¿etowe (ekonomiczne)', 22.69 ,0.08,'MNIS');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch serwowany - Menu I', 23.77 ,0.08,'MNIS');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch serwowany - Menu II', 25.93 ,0.08 ,'MNIS');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch serwowany - Menu III', 27.01 ,0.08 ,'MNIS');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch serwowany - Menu IV', 28.09 ,0.08 ,'MNIS');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch serwowany - Menu V', 29.18 ,0.08 ,'MNIS');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch bufetowy - Menu I', 25.93 ,0.08 ,'MNIS');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch bufetowy - Menu II', 27.01 ,0.08 ,'MNIS');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch bufetowy - Menu III', 28.09 ,0.08,'MNIS');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch bufetowy - Menu IV', 29.18 ,0.08,'MNIS');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Lunch bufetowy - Menu V', 30.26 ,0.08,'MNIS');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Kolacja bufetowa - Menu I', 22.69 ,0.08,'MNIS');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Kolacja bufetowa - Menu II', 23.77 ,0.08 ,'MNIS');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Kolacja bufetowa - Menu III', 25.93 ,0.08 ,'MNIS');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('Kolacja bufetowa - Menu IV', 28.09 ,0.08 ,'MNIS');
																			
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('OpenBar: STANDARD do 4h', 56.02 ,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('OpenBar: STANDARD od 4h do 8h', 84.02 ,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('OpenBar: STANDARD powy¿ej 8h', 112.03 ,0.23 ,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('OpenBar: PREMIUM STANDARD do 4h', 67.69 ,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('OpenBar: PREMIUM od 4h do 8h', 101.53 ,0.23,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('OpenBar: PREMIUM powy¿ej 8h', 135.37 ,0.23 ,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('OpenBar: DIAMENT do 4h', 79.36 ,0.23 ,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('OpenBar: DIAMENT od 4h do 8h', 119.03 ,0.23 ,'MALK');
insert PropMenuGastronomicThings_Dictionary_First (ThingName,NettoMini,Vat,MergeType) values ('OpenBar: DIAMENT powy¿ej 8h', 158.71 ,0.23 ,'MALK');

insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,Febuary,March,April,May,June,July,August,September,October,November,December,Other) values ('A',400,400,600,400,600,300,300,400,600,600,400,300,1800);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,Febuary,March,April,May,June,July,August,September,October,November,December,Other) values ('B',500,500,800,600,800,375,375,500,800,800,600,375,2400);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,Febuary,March,April,May,June,July,August,September,October,November,December,Other) values ('A+B',900,900,1500,1000,1500,675,675,900,1500,1500,1000,675,4500);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,Febuary,March,April,May,June,July,August,September,October,November,December,Other) values ('C',400,400,700,500,700,300,300,400,700,700,500,300,2100);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,Febuary,March,April,May,June,July,August,September,October,November,December,Other) values ('D',600,600,1000,800,1000,450,450,600,1000,1000,800,450,3000);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,Febuary,March,April,May,June,July,August,September,October,November,December,Other) values ('C+D',1000,1000,1600,1300,1600,750,750,1000,1600,1600,1300,750,4800);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,Febuary,March,April,May,June,July,August,September,October,November,December,Other) values ('A+C',800,800,1000,1000,1000,600,600,800,1000,1000,1000,600,3000);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,Febuary,March,April,May,June,July,August,September,October,November,December,Other) values ('B+D',1100,1100,1300,1400,1300,825,825,1100,1300,1300,1400,825,3900);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,Febuary,March,April,May,June,July,August,September,October,November,December,Other) values ('DIAMENT',2500,2500,4000,3000,4000,1750,1750,2500,4000,4000,3000,1750,12000);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,Febuary,March,April,May,June,July,August,September,October,November,December,Other) values ('KORAL',300,300,600,400,600,250,250,300,600,600,400,250,1800);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,Febuary,March,April,May,June,July,August,September,October,November,December,Other) values ('Atmosfera',1300,1300,1900,1500,1900,900,900,1300,1900,1900,1500,900,5700);
insert into PropReservationDetails_Dictionary_HallPrices (Hall,January,Febuary,March,April,May,June,July,August,September,October,November,December,Other) values ('VIP',400,400,700,500,700,300,300,400,700,700,500,300,2100);

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
insert into [PropReservationDetails_Dictionary_HallSettings] (Setting) values ('Szkolne')
insert into [PropReservationDetails_Dictionary_HallSettings] (Setting) values ('Teatr')
insert into [PropReservationDetails_Dictionary_HallSettings] (Setting) values ('Prezentacja')
insert into [PropReservationDetails_Dictionary_HallSettings] (Setting) values ('Inne')
insert into [PropReservationDetails_Dictionary_HallSettings] (Setting) values ('Do ustalenia')
insert into [PropReservationDetails_Dictionary_HallSettings] (Setting) values ('Bankiet')
insert into [PropReservationDetails_Dictionary_HallSettings] (Setting) values ('Wspólny stó³')
insert into [PropReservationDetails_Dictionary_HallSettings] (Setting) values ('Teatr / Bankiet')

insert into [VatList] (Vat) values ('8');
insert into [VatList] (Vat) values ('23');

update PropMenuGastronomicThings_Dictionary_First set Vat = '23' where MergeType = 'MALK'
update PropMenuGastronomicThings_Dictionary_First set Vat = '23' where MergeType = 'MGA23'
update PropMenuGastronomicThings_Dictionary_First set Vat = '8' where MergeType = 'MNIS'
update PropMenuGastronomicThings_Dictionary_First set Vat = '8' where MergeType = 'MGA8'