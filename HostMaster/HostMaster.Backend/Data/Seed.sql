SET IDENTITY_INSERT [dbo].[Countries] ON 
INSERT [dbo].[Countries] ([Id], [Name]) VALUES (1, N'Colombia')
SET IDENTITY_INSERT [dbo].[Countries] OFF

SET IDENTITY_INSERT [dbo].[States] ON 
INSERT [dbo].[States] ([Id], [Name],[CountryId]) VALUES (1, N'Santander',1)
SET IDENTITY_INSERT [dbo].[States] OFF

SET IDENTITY_INSERT [dbo].[Cities] ON
INSERT [dbo].[Cities] ([Id], [Name], [StateId]) VALUES (1, N'Barbosa', 1)
SET IDENTITY_INSERT [dbo].[Cities] OFF

SET IDENTITY_INSERT [dbo].[Accommodations] ON 
INSERT [dbo].[Accommodations] ([Id], [Name], [Address], [PhoneNumber], [CityId]) VALUES (1, N'Hotel Barbosa', N'carrera 9 centro', N'3112515555',1)
SET IDENTITY_INSERT [dbo].[Accommodations] OFF

SET IDENTITY_INSERT [dbo].[Customers] ON 
INSERT [dbo].[Customers] ([Id], [FirstName], [LastName], [DocumentType], [DocumentNumber], [Email], [PhoneNumber]) VALUES (1, N'Oliver Camilo',N'Prieto Garcia','CC',1234567,N'caprirey4@gmail.com',N'3044315484')
SET IDENTITY_INSERT [dbo].[Customers] OFF

SET IDENTITY_INSERT [dbo].[Employees] ON 
INSERT [dbo].[Employees] ([Id], [FirstName], [LastName], [Position],[Email],[PhoneNumber],[AccommodationId]) VALUES (1, N'Nestor Camilo',N'Prieto Reyes', N'Administrator',N'caprirey4@gmail.com',N'3044315484',1 )
SET IDENTITY_INSERT [dbo].[Employees] OFF

SET IDENTITY_INSERT [dbo].[ExtraServices] ON 
INSERT [dbo].[ExtraServices] ([Id], [ServiceName],[Price]) VALUES (1, N'Tour Ciudad', 99.99)
SET IDENTITY_INSERT [dbo].[ExtraServices] OFF

SET IDENTITY_INSERT [dbo].[Reservations] ON 
INSERT [dbo].[Reservations] ([Id], [StartDate],[EndDate],[NumberOfGuests],[CustomerId],[AccommodationId], [State]) VALUES (1,'2024-09-15T14:30:00.1234567','2024-09-17T14:30:00.1234567',2,1,1, 'Created')
SET IDENTITY_INSERT [dbo].[Reservations] OFF

INSERT INTO [dbo].[ExtraServiceReservation] ([ExtraServicesId], [ReservationsId]) VALUES (1, 1)

SET IDENTITY_INSERT [dbo].[Payments] ON 
INSERT [dbo].[Payments] ([Id], [Amount],[Taxes],[PaymentDate],[PaymentMethod],[ReservationId]) VALUES (1, 99.99,19,'2024-09-15T14:30:00.1234567',N'EFECTIVO',1)
SET IDENTITY_INSERT [dbo].[Payments] OFF

SET IDENTITY_INSERT [dbo].[RoomTypes] ON 
INSERT [dbo].[RoomTypes] ([Id], [TypeName],[Description],[Price],[MaxGuests]) VALUES (1, N'SINGLE',N'Una cama sencilla',60.00,2)
INSERT [dbo].[RoomTypes] ([Id], [TypeName],[Description],[Price],[MaxGuests]) VALUES (2, N'DOUBLE',N'Una cama doble',80.00,2)
INSERT [dbo].[RoomTypes] ([Id], [TypeName],[Description],[Price],[MaxGuests]) VALUES (3, N'DOUBLE 2 BEDS',N'Dos camas dobles',80.00,4)
SET IDENTITY_INSERT [dbo].[RoomTypes] OFF

SET IDENTITY_INSERT [dbo].[Rooms] ON 
INSERT [dbo].[Rooms] ([Id], [RoomNumber],[IsAvailable],[AccommodationId],[RoomTypeId]) VALUES (1, N'101',1,1,1)
SET IDENTITY_INSERT [dbo].[Rooms] OFF

SET IDENTITY_INSERT [dbo].[Rooms] ON 
INSERT [dbo].[Rooms] ([Id], [RoomNumber],[IsAvailable],[AccommodationId],[RoomTypeId]) VALUES (2, N'201',1,1,2)
SET IDENTITY_INSERT [dbo].[Rooms] OFF

SET IDENTITY_INSERT [dbo].[RoomInventoryItems] ON 
INSERT [dbo].[RoomInventoryItems] ([Id], [ItemName],[Quantity],[Condition],[RoomId]) VALUES (1, N'Cama',1,N'Ok',1)
SET IDENTITY_INSERT [dbo].[RoomInventoryItems] OFF

-- SET IDENTITY_INSERT [dbo].[RoomPhotos] ON 
-- INSERT [dbo].[RoomPhotos] ([Id], [RoomPhotoUrl],[RoomId]) VALUES (1, N'UUID-1',1)
-- SET IDENTITY_INSERT [dbo].[RoomPhotos] OFF

INSERT INTO [dbo].[ReservationRoom] ([ReservationsId], [RoomsId]) VALUES (1, 1);
