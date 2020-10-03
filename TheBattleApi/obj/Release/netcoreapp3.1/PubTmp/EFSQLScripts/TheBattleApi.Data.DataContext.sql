IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetRoles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(256) NULL,
        [NormalizedName] nvarchar(256) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoles] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUsers] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(256) NULL,
        [NormalizedUserName] nvarchar(256) NULL,
        [Email] nvarchar(256) NULL,
        [NormalizedEmail] nvarchar(256) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_AspNetUsers] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetRoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetRoleClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetRoleClaims_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NOT NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserClaims] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_AspNetUserClaims_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserLogins] (
        [LoginProvider] nvarchar(128) NOT NULL,
        [ProviderKey] nvarchar(128) NOT NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        [UserId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserLogins] PRIMARY KEY ([LoginProvider], [ProviderKey]),
        CONSTRAINT [FK_AspNetUserLogins_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserRoles] (
        [UserId] nvarchar(450) NOT NULL,
        [RoleId] nvarchar(450) NOT NULL,
        CONSTRAINT [PK_AspNetUserRoles] PRIMARY KEY ([UserId], [RoleId]),
        CONSTRAINT [FK_AspNetUserRoles_AspNetRoles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [AspNetRoles] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_AspNetUserRoles_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE TABLE [AspNetUserTokens] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(128) NOT NULL,
        [Name] nvarchar(128) NOT NULL,
        [Value] nvarchar(max) NULL,
        CONSTRAINT [PK_AspNetUserTokens] PRIMARY KEY ([UserId], [LoginProvider], [Name]),
        CONSTRAINT [FK_AspNetUserTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetRoleClaims_RoleId] ON [AspNetRoleClaims] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE UNIQUE INDEX [RoleNameIndex] ON [AspNetRoles] ([NormalizedName]) WHERE [NormalizedName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetUserClaims_UserId] ON [AspNetUserClaims] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetUserLogins_UserId] ON [AspNetUserLogins] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [IX_AspNetUserRoles_RoleId] ON [AspNetUserRoles] ([RoleId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE INDEX [EmailIndex] ON [AspNetUsers] ([NormalizedEmail]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    CREATE UNIQUE INDEX [UserNameIndex] ON [AspNetUsers] ([NormalizedUserName]) WHERE [NormalizedUserName] IS NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'00000000000000_CreateIdentitySchema')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'00000000000000_CreateIdentitySchema', N'3.1.7');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920121201_AddedRefreshTokens')
BEGIN
    CREATE TABLE [RefreshTokens] (
        [Token] nvarchar(450) NOT NULL,
        [JwtId] nvarchar(max) NULL,
        [CreationDate] datetime2 NOT NULL,
        [ExpiryDate] datetime2 NOT NULL,
        [Used] bit NOT NULL,
        [Invalidated] bit NOT NULL,
        [UserId] nvarchar(450) NULL,
        CONSTRAINT [PK_RefreshTokens] PRIMARY KEY ([Token]),
        CONSTRAINT [FK_RefreshTokens_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920121201_AddedRefreshTokens')
BEGIN
    CREATE INDEX [IX_RefreshTokens_UserId] ON [RefreshTokens] ([UserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920121201_AddedRefreshTokens')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200920121201_AddedRefreshTokens', N'3.1.7');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920141228_AddMainModel')
BEGIN
    CREATE TABLE [Rooms] (
        [Id] nvarchar(450) NOT NULL,
        [Size] int NOT NULL,
        [IsHostTurn] bit NOT NULL,
        [HostUserId] nvarchar(450) NULL,
        [GuestUserId] nvarchar(450) NULL,
        CONSTRAINT [PK_Rooms] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Rooms_AspNetUsers_GuestUserId] FOREIGN KEY ([GuestUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION,
        CONSTRAINT [FK_Rooms_AspNetUsers_HostUserId] FOREIGN KEY ([HostUserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920141228_AddMainModel')
BEGIN
    CREATE TABLE [ShipTypes] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [OffsetX] int NOT NULL,
        [OffsetY] int NOT NULL,
        CONSTRAINT [PK_ShipTypes] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920141228_AddMainModel')
BEGIN
    CREATE TABLE [WeaponTypes] (
        [Id] int NOT NULL IDENTITY,
        [Name] nvarchar(max) NULL,
        [Power] int NOT NULL,
        [IsMissile] bit NOT NULL,
        CONSTRAINT [PK_WeaponTypes] PRIMARY KEY ([Id])
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920141228_AddMainModel')
BEGIN
    CREATE TABLE [Maps] (
        [UserId] nvarchar(450) NOT NULL,
        [RoomId] nvarchar(450) NOT NULL,
        [IsCOmpleted] bit NOT NULL,
        CONSTRAINT [pk_map] PRIMARY KEY ([UserId], [RoomId]),
        CONSTRAINT [FK_Maps_Rooms_RoomId] FOREIGN KEY ([RoomId]) REFERENCES [Rooms] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_Maps_AspNetUsers_UserId] FOREIGN KEY ([UserId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920141228_AddMainModel')
BEGIN
    CREATE TABLE [ShipGroups] (
        [UserId] nvarchar(450) NOT NULL,
        [RoomId] nvarchar(450) NOT NULL,
        [ShipTypeId] int NOT NULL,
        [Count] int NOT NULL,
        [Limit] int NOT NULL,
        CONSTRAINT [pk_ship_group] PRIMARY KEY ([UserId], [RoomId], [ShipTypeId]),
        CONSTRAINT [FK_ShipGroups_ShipTypes_ShipTypeId] FOREIGN KEY ([ShipTypeId]) REFERENCES [ShipTypes] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_ShipGroups_Maps_UserId_RoomId] FOREIGN KEY ([UserId], [RoomId]) REFERENCES [Maps] ([UserId], [RoomId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920141228_AddMainModel')
BEGIN
    CREATE TABLE [WeaponGroups] (
        [UserId] nvarchar(450) NOT NULL,
        [RoomId] nvarchar(450) NOT NULL,
        [WeaponTypeId] int NOT NULL,
        [Count] int NOT NULL,
        [Limit] int NOT NULL,
        CONSTRAINT [pk_weapon_group] PRIMARY KEY ([UserId], [RoomId], [WeaponTypeId]),
        CONSTRAINT [FK_WeaponGroups_WeaponTypes_WeaponTypeId] FOREIGN KEY ([WeaponTypeId]) REFERENCES [WeaponTypes] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_WeaponGroups_Maps_UserId_RoomId] FOREIGN KEY ([UserId], [RoomId]) REFERENCES [Maps] ([UserId], [RoomId]) ON DELETE CASCADE
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920141228_AddMainModel')
BEGIN
    CREATE TABLE [Ships] (
        [Id] int NOT NULL IDENTITY,
        [X] int NOT NULL,
        [Y] int NOT NULL,
        [HP] float NOT NULL,
        [UserId] nvarchar(max) NULL,
        [RoomId] nvarchar(max) NULL,
        [ShipTypeId] int NOT NULL,
        [ShipGroupUserId] nvarchar(450) NULL,
        [ShipGroupRoomId] nvarchar(450) NULL,
        [ShipGroupShipTypeId] int NULL,
        CONSTRAINT [PK_Ships] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Ships_ShipGroups_ShipGroupUserId_ShipGroupRoomId_ShipGroupShipTypeId] FOREIGN KEY ([ShipGroupUserId], [ShipGroupRoomId], [ShipGroupShipTypeId]) REFERENCES [ShipGroups] ([UserId], [RoomId], [ShipTypeId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920141228_AddMainModel')
BEGIN
    CREATE TABLE [Weapons] (
        [Id] int NOT NULL IDENTITY,
        [X] int NOT NULL,
        [Y] int NOT NULL,
        [IsUsed] bit NOT NULL,
        [UserId] nvarchar(max) NULL,
        [RoomId] nvarchar(max) NULL,
        [WeaponTypeId] int NOT NULL,
        [WeaponGroupUserId] nvarchar(450) NULL,
        [WeaponGroupRoomId] nvarchar(450) NULL,
        [WeaponGroupWeaponTypeId] int NULL,
        CONSTRAINT [PK_Weapons] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Weapons_WeaponGroups_WeaponGroupUserId_WeaponGroupRoomId_WeaponGroupWeaponTypeId] FOREIGN KEY ([WeaponGroupUserId], [WeaponGroupRoomId], [WeaponGroupWeaponTypeId]) REFERENCES [WeaponGroups] ([UserId], [RoomId], [WeaponTypeId]) ON DELETE NO ACTION
    );
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920141228_AddMainModel')
BEGIN
    CREATE INDEX [IX_Maps_RoomId] ON [Maps] ([RoomId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920141228_AddMainModel')
BEGIN
    CREATE INDEX [IX_Rooms_GuestUserId] ON [Rooms] ([GuestUserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920141228_AddMainModel')
BEGIN
    CREATE INDEX [IX_Rooms_HostUserId] ON [Rooms] ([HostUserId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920141228_AddMainModel')
BEGIN
    CREATE INDEX [IX_ShipGroups_ShipTypeId] ON [ShipGroups] ([ShipTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920141228_AddMainModel')
BEGIN
    CREATE INDEX [IX_Ships_ShipGroupUserId_ShipGroupRoomId_ShipGroupShipTypeId] ON [Ships] ([ShipGroupUserId], [ShipGroupRoomId], [ShipGroupShipTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920141228_AddMainModel')
BEGIN
    CREATE INDEX [IX_WeaponGroups_WeaponTypeId] ON [WeaponGroups] ([WeaponTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920141228_AddMainModel')
BEGIN
    CREATE INDEX [IX_Weapons_WeaponGroupUserId_WeaponGroupRoomId_WeaponGroupWeaponTypeId] ON [Weapons] ([WeaponGroupUserId], [WeaponGroupRoomId], [WeaponGroupWeaponTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920141228_AddMainModel')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200920141228_AddMainModel', N'3.1.7');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920143401_RemovingSomeAutoGeneratedColumns')
BEGIN
    ALTER TABLE [Ships] DROP CONSTRAINT [FK_Ships_ShipGroups_ShipGroupUserId_ShipGroupRoomId_ShipGroupShipTypeId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920143401_RemovingSomeAutoGeneratedColumns')
BEGIN
    ALTER TABLE [Weapons] DROP CONSTRAINT [FK_Weapons_WeaponGroups_WeaponGroupUserId_WeaponGroupRoomId_WeaponGroupWeaponTypeId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920143401_RemovingSomeAutoGeneratedColumns')
BEGIN
    DROP INDEX [IX_Weapons_WeaponGroupUserId_WeaponGroupRoomId_WeaponGroupWeaponTypeId] ON [Weapons];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920143401_RemovingSomeAutoGeneratedColumns')
BEGIN
    DROP INDEX [IX_Ships_ShipGroupUserId_ShipGroupRoomId_ShipGroupShipTypeId] ON [Ships];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920143401_RemovingSomeAutoGeneratedColumns')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Weapons]') AND [c].[name] = N'WeaponGroupRoomId');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Weapons] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Weapons] DROP COLUMN [WeaponGroupRoomId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920143401_RemovingSomeAutoGeneratedColumns')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Weapons]') AND [c].[name] = N'WeaponGroupUserId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Weapons] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Weapons] DROP COLUMN [WeaponGroupUserId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920143401_RemovingSomeAutoGeneratedColumns')
BEGIN
    DECLARE @var2 sysname;
    SELECT @var2 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Weapons]') AND [c].[name] = N'WeaponGroupWeaponTypeId');
    IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Weapons] DROP CONSTRAINT [' + @var2 + '];');
    ALTER TABLE [Weapons] DROP COLUMN [WeaponGroupWeaponTypeId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920143401_RemovingSomeAutoGeneratedColumns')
BEGIN
    DECLARE @var3 sysname;
    SELECT @var3 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ships]') AND [c].[name] = N'ShipGroupRoomId');
    IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Ships] DROP CONSTRAINT [' + @var3 + '];');
    ALTER TABLE [Ships] DROP COLUMN [ShipGroupRoomId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920143401_RemovingSomeAutoGeneratedColumns')
BEGIN
    DECLARE @var4 sysname;
    SELECT @var4 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ships]') AND [c].[name] = N'ShipGroupShipTypeId');
    IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Ships] DROP CONSTRAINT [' + @var4 + '];');
    ALTER TABLE [Ships] DROP COLUMN [ShipGroupShipTypeId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920143401_RemovingSomeAutoGeneratedColumns')
BEGIN
    DECLARE @var5 sysname;
    SELECT @var5 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ships]') AND [c].[name] = N'ShipGroupUserId');
    IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Ships] DROP CONSTRAINT [' + @var5 + '];');
    ALTER TABLE [Ships] DROP COLUMN [ShipGroupUserId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920143401_RemovingSomeAutoGeneratedColumns')
BEGIN
    DECLARE @var6 sysname;
    SELECT @var6 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Weapons]') AND [c].[name] = N'UserId');
    IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Weapons] DROP CONSTRAINT [' + @var6 + '];');
    ALTER TABLE [Weapons] ALTER COLUMN [UserId] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920143401_RemovingSomeAutoGeneratedColumns')
BEGIN
    DECLARE @var7 sysname;
    SELECT @var7 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Weapons]') AND [c].[name] = N'RoomId');
    IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Weapons] DROP CONSTRAINT [' + @var7 + '];');
    ALTER TABLE [Weapons] ALTER COLUMN [RoomId] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920143401_RemovingSomeAutoGeneratedColumns')
BEGIN
    DECLARE @var8 sysname;
    SELECT @var8 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ships]') AND [c].[name] = N'UserId');
    IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Ships] DROP CONSTRAINT [' + @var8 + '];');
    ALTER TABLE [Ships] ALTER COLUMN [UserId] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920143401_RemovingSomeAutoGeneratedColumns')
BEGIN
    DECLARE @var9 sysname;
    SELECT @var9 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ships]') AND [c].[name] = N'RoomId');
    IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Ships] DROP CONSTRAINT [' + @var9 + '];');
    ALTER TABLE [Ships] ALTER COLUMN [RoomId] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920143401_RemovingSomeAutoGeneratedColumns')
BEGIN
    CREATE INDEX [IX_Weapons_UserId_RoomId_WeaponTypeId] ON [Weapons] ([UserId], [RoomId], [WeaponTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920143401_RemovingSomeAutoGeneratedColumns')
BEGIN
    CREATE INDEX [IX_Ships_UserId_RoomId_ShipTypeId] ON [Ships] ([UserId], [RoomId], [ShipTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920143401_RemovingSomeAutoGeneratedColumns')
BEGIN
    ALTER TABLE [Ships] ADD CONSTRAINT [fk_ship_group] FOREIGN KEY ([UserId], [RoomId], [ShipTypeId]) REFERENCES [ShipGroups] ([UserId], [RoomId], [ShipTypeId]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920143401_RemovingSomeAutoGeneratedColumns')
BEGIN
    ALTER TABLE [Weapons] ADD CONSTRAINT [fk_weapon_group] FOREIGN KEY ([UserId], [RoomId], [WeaponTypeId]) REFERENCES [WeaponGroups] ([UserId], [RoomId], [WeaponTypeId]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200920143401_RemovingSomeAutoGeneratedColumns')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200920143401_RemovingSomeAutoGeneratedColumns', N'3.1.7');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200921075737_RoomPKChange')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200921075737_RoomPKChange', N'3.1.7');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200921084823_ModelChange1')
BEGIN
    DECLARE @var10 sysname;
    SELECT @var10 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[WeaponTypes]') AND [c].[name] = N'IsMissile');
    IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [WeaponTypes] DROP CONSTRAINT [' + @var10 + '];');
    ALTER TABLE [WeaponTypes] DROP COLUMN [IsMissile];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200921084823_ModelChange1')
BEGIN
    DECLARE @var11 sysname;
    SELECT @var11 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShipTypes]') AND [c].[name] = N'OffsetX');
    IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [ShipTypes] DROP CONSTRAINT [' + @var11 + '];');
    ALTER TABLE [ShipTypes] DROP COLUMN [OffsetX];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200921084823_ModelChange1')
BEGIN
    DECLARE @var12 sysname;
    SELECT @var12 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[ShipTypes]') AND [c].[name] = N'OffsetY');
    IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [ShipTypes] DROP CONSTRAINT [' + @var12 + '];');
    ALTER TABLE [ShipTypes] DROP COLUMN [OffsetY];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200921084823_ModelChange1')
BEGIN
    ALTER TABLE [WeaponTypes] ADD [IsMine] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200921084823_ModelChange1')
BEGIN
    ALTER TABLE [ShipTypes] ADD [Size] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200921084823_ModelChange1')
BEGIN
    ALTER TABLE [Ships] ADD [IsHorizontal] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200921084823_ModelChange1')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200921084823_ModelChange1', N'3.1.7');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200921084929_WeaponTypeAndShipTypeData')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'Size') AND [object_id] = OBJECT_ID(N'[ShipTypes]'))
        SET IDENTITY_INSERT [ShipTypes] ON;
    INSERT INTO [ShipTypes] ([Id], [Name], [Size])
    VALUES (1, N'x1', 1),
    (2, N'x2', 2),
    (3, N'x3', 3),
    (4, N'x4', 4);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'Name', N'Size') AND [object_id] = OBJECT_ID(N'[ShipTypes]'))
        SET IDENTITY_INSERT [ShipTypes] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200921084929_WeaponTypeAndShipTypeData')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsMine', N'Name', N'Power') AND [object_id] = OBJECT_ID(N'[WeaponTypes]'))
        SET IDENTITY_INSERT [WeaponTypes] ON;
    INSERT INTO [WeaponTypes] ([Id], [IsMine], [Name], [Power])
    VALUES (1, CAST(1 AS bit), N'Mine', 1),
    (2, CAST(0 AS bit), N'Bullet', 1),
    (3, CAST(0 AS bit), N'Bomb', 1),
    (4, CAST(0 AS bit), N'Torpedo', 1),
    (5, CAST(0 AS bit), N'Missile', 1);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsMine', N'Name', N'Power') AND [object_id] = OBJECT_ID(N'[WeaponTypes]'))
        SET IDENTITY_INSERT [WeaponTypes] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20200921084929_WeaponTypeAndShipTypeData')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20200921084929_WeaponTypeAndShipTypeData', N'3.1.7');
END;

GO

