SET IDENTITY_INSERT [dbo].[Country] ON 
INSERT [dbo].[Country] ([Id], [Name]) VALUES (1, N'Colombia')
SET IDENTITY_INSERT [dbo].[Country] OFF

SET IDENTITY_INSERT [dbo].[State] ON 
INSERT [dbo].[State] ([Id], [Name],[CountryId]) VALUES (1, N'Santander',1)
SET IDENTITY_INSERT [dbo].[State] OFF

SET IDENTITY_INSERT [dbo].[City] ON
INSERT [dbo].[City] ([Id], [Name], [StateId]) VALUES (1, N'Barbosa', 1)
SET IDENTITY_INSERT [dbo].[City] OFF

SET IDENTITY_INSERT [dbo].[Accommodation] ON 
INSERT [dbo].[Accommodation] ([Id], [Name], [Address], [PhoneNumber], [CityId]) VALUES (1, N'Hotel Barbosa', N'carrera 9 centro', N'3112515555',1)
SET IDENTITY_INSERT [dbo].[Accommodation] OFF

SET IDENTITY_INSERT [dbo].[Customer] ON 
INSERT [dbo].[Customer] ([Id], [FirstName], [LastName], [Email], [PhoneNumber]) VALUES (1, N'Oliver Camilo',N'Prieto Garcia',N'caprirey4@gmail.com',N'3044315484')
SET IDENTITY_INSERT [dbo].[Customer] OFF

SET IDENTITY_INSERT [dbo].[Employee] ON 
INSERT [dbo].[Employee] ([Id], [FirstName], [LastName], [Position],[Email],[PhoneNumber],[AccommodationId]) VALUES (1, N'Nestor Camilo',N'Prieto Reyes', N'Administrator',N'caprirey4@gmail.com',N'3044315484',1 )
SET IDENTITY_INSERT [dbo].[Employee] OFF

SET IDENTITY_INSERT [dbo].[ExtraService] ON 
INSERT [dbo].[ExtraService] ([Id], [ServiceName],[Price]) VALUES (1, N'Tour Ciudad', 99.99)
SET IDENTITY_INSERT [dbo].[ExtraService] OFF

SET IDENTITY_INSERT [dbo].[Reservations] ON 
INSERT [dbo].[Reservations] ([Id], [StartDate],[EndDate],[NumberOfGuests],[CustomerId],[AccommodationId]) VALUES (1,'2024-09-15T14:30:00.1234567','2024-09-17T14:30:00.1234567',2,1,1)
SET IDENTITY_INSERT [dbo].[Reservations] OFF

INSERT INTO [dbo].[ExtraServiceReservation] ([ExtraServicesId], [ReservationsId]) VALUES (1, 1)

SET IDENTITY_INSERT [dbo].[Payment] ON 
INSERT [dbo].[Payment] ([Id], [Amount],[PaymentDate],[PaymentMethod],[ReservationId]) VALUES (1, 99.99,'2024-09-15T14:30:00.1234567',N'EFECTIVO',1)
SET IDENTITY_INSERT [dbo].[Payment] OFF

SET IDENTITY_INSERT [dbo].[RoomType] ON 
INSERT [dbo].[RoomType] ([Id], [TypeName],[Description]) VALUES (1, N'DOBLE',N'Posibilidad para 2 personas')
INSERT [dbo].[RoomType] ([Id], [TypeName],[Description]) VALUES (2, N'DOBLE 2 CAMAS',N'Posibilidad para 4 personas')
SET IDENTITY_INSERT [dbo].[RoomType] OFF

SET IDENTITY_INSERT [dbo].[Rooms] ON 
INSERT [dbo].[Rooms] ([Id], [RoomNumber],[Price],[IsAvailable],[AccommodationId],[RoomTypeId]) VALUES (1, N'101',80.00,1,1,1)
SET IDENTITY_INSERT [dbo].[Rooms] OFF

SET IDENTITY_INSERT [dbo].[Rooms] ON 
INSERT [dbo].[Rooms] ([Id], [RoomNumber],[Price],[IsAvailable],[AccommodationId],[RoomTypeId]) VALUES (2, N'201',80.00,1,1,2)
SET IDENTITY_INSERT [dbo].[Rooms] OFF

SET IDENTITY_INSERT [dbo].[RoomInventoryItem] ON 
INSERT [dbo].[RoomInventoryItem] ([Id], [ItemName],[Quantity],[Condition],[RoomId]) VALUES (1, N'Cama',1,N'Ok',1)
SET IDENTITY_INSERT [dbo].[RoomInventoryItem] OFF

SET IDENTITY_INSERT [dbo].[RoomPhoto] ON 
INSERT [dbo].[RoomPhoto] ([Id], [RoomPhotoName],[RoomId]) VALUES (1, N'UUID-1',1)
SET IDENTITY_INSERT [dbo].[RoomPhoto] OFF

INSERT INTO [dbo].[ReservationRoom] ([ReservationsId], [RoomsId]) VALUES (1, 1);
