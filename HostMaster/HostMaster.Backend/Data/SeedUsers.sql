﻿SET IDENTITY_INSERT [dbo].[Users] ON;

INSERT [dbo].[Users] ([Id], [Email], [DocumentType], [Document], [FirstName], [LastName], [Photo], [PhoneNumber], [UserType]) 
VALUES 
    (1, 'alice.green@example.com', 'CC', '444556677', 'Alice', 'Green', NULL, '555-0606', 1), 
    (2, 'robert.jones@example.com', 'TI', '555667788', 'Robert', 'Jones', NULL, '555-0707', 1), 
    (3, 'linda.white@example.com', 'CC', '666778899', 'Linda', 'White', NULL, '555-0808', 1), 
    (4, 'charles.adams@example.com', 'CC', '777889900', 'Charles', 'Adams', NULL, '555-0909', 1), 
    (5, 'patricia.thomas@example.com', 'TI', '888990011', 'Patricia', 'Thomas', NULL, '555-1010', 1);

SET IDENTITY_INSERT [dbo].[Users] OFF;