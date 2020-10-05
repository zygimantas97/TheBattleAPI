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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003065948_RemoveShipAndWeaponTypes')
BEGIN
    DELETE FROM [ShipTypes]
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003065948_RemoveShipAndWeaponTypes')
BEGIN
    DELETE FROM [ShipTypes]
    WHERE [Id] = 2;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003065948_RemoveShipAndWeaponTypes')
BEGIN
    DELETE FROM [ShipTypes]
    WHERE [Id] = 3;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003065948_RemoveShipAndWeaponTypes')
BEGIN
    DELETE FROM [ShipTypes]
    WHERE [Id] = 4;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003065948_RemoveShipAndWeaponTypes')
BEGIN
    DELETE FROM [WeaponTypes]
    WHERE [Id] = 1;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003065948_RemoveShipAndWeaponTypes')
BEGIN
    DELETE FROM [WeaponTypes]
    WHERE [Id] = 2;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003065948_RemoveShipAndWeaponTypes')
BEGIN
    DELETE FROM [WeaponTypes]
    WHERE [Id] = 3;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003065948_RemoveShipAndWeaponTypes')
BEGIN
    DELETE FROM [WeaponTypes]
    WHERE [Id] = 4;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003065948_RemoveShipAndWeaponTypes')
BEGIN
    DELETE FROM [WeaponTypes]
    WHERE [Id] = 5;
    SELECT @@ROWCOUNT;

END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003065948_RemoveShipAndWeaponTypes')
BEGIN
    EXEC sp_rename N'[Maps].[IsCOmpleted]', N'IsCompleted', N'COLUMN';
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003065948_RemoveShipAndWeaponTypes')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201003065948_RemoveShipAndWeaponTypes', N'3.1.7');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003070633_AddedWeaponTypes')
BEGIN
    ALTER TABLE [ShipTypes] ADD [IsSubmarine] bit NOT NULL DEFAULT CAST(0 AS bit);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003070633_AddedWeaponTypes')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsMine', N'Name', N'Power') AND [object_id] = OBJECT_ID(N'[WeaponTypes]'))
        SET IDENTITY_INSERT [WeaponTypes] ON;
    INSERT INTO [WeaponTypes] ([Id], [IsMine], [Name], [Power])
    VALUES (1, CAST(0 AS bit), N'Bomb', 1);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsMine', N'Name', N'Power') AND [object_id] = OBJECT_ID(N'[WeaponTypes]'))
        SET IDENTITY_INSERT [WeaponTypes] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003070633_AddedWeaponTypes')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsMine', N'Name', N'Power') AND [object_id] = OBJECT_ID(N'[WeaponTypes]'))
        SET IDENTITY_INSERT [WeaponTypes] ON;
    INSERT INTO [WeaponTypes] ([Id], [IsMine], [Name], [Power])
    VALUES (2, CAST(0 AS bit), N'Torpedo', 1);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsMine', N'Name', N'Power') AND [object_id] = OBJECT_ID(N'[WeaponTypes]'))
        SET IDENTITY_INSERT [WeaponTypes] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003070633_AddedWeaponTypes')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsMine', N'Name', N'Power') AND [object_id] = OBJECT_ID(N'[WeaponTypes]'))
        SET IDENTITY_INSERT [WeaponTypes] ON;
    INSERT INTO [WeaponTypes] ([Id], [IsMine], [Name], [Power])
    VALUES (3, CAST(1 AS bit), N'Mine', 1);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsMine', N'Name', N'Power') AND [object_id] = OBJECT_ID(N'[WeaponTypes]'))
        SET IDENTITY_INSERT [WeaponTypes] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003070633_AddedWeaponTypes')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201003070633_AddedWeaponTypes', N'3.1.7');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003070839_AddedShipTypes')
BEGIN
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsSubmarine', N'Name', N'Size') AND [object_id] = OBJECT_ID(N'[ShipTypes]'))
        SET IDENTITY_INSERT [ShipTypes] ON;
    INSERT INTO [ShipTypes] ([Id], [IsSubmarine], [Name], [Size])
    VALUES (1, CAST(0 AS bit), N'Small Destroyer', 1),
    (2, CAST(0 AS bit), N'Medium Destroyer', 2),
    (3, CAST(0 AS bit), N'Large Destroyer', 3),
    (4, CAST(0 AS bit), N'Atomic Destroyer', 4),
    (5, CAST(1 AS bit), N'Small Submarine', 1),
    (6, CAST(1 AS bit), N'Medium Submarine', 2),
    (7, CAST(1 AS bit), N'Large Submarine', 3),
    (8, CAST(1 AS bit), N'Atomic Submarine', 4);
    IF EXISTS (SELECT * FROM [sys].[identity_columns] WHERE [name] IN (N'Id', N'IsSubmarine', N'Name', N'Size') AND [object_id] = OBJECT_ID(N'[ShipTypes]'))
        SET IDENTITY_INSERT [ShipTypes] OFF;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003070839_AddedShipTypes')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201003070839_AddedShipTypes', N'3.1.7');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003072949_RemovedWeaponGroups')
BEGIN
    ALTER TABLE [Weapons] DROP CONSTRAINT [fk_weapon_group];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003072949_RemovedWeaponGroups')
BEGIN
    DROP TABLE [WeaponGroups];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003072949_RemovedWeaponGroups')
BEGIN
    DROP INDEX [IX_Weapons_UserId_RoomId_WeaponTypeId] ON [Weapons];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003072949_RemovedWeaponGroups')
BEGIN
    DECLARE @var13 sysname;
    SELECT @var13 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Ships]') AND [c].[name] = N'IsHorizontal');
    IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Ships] DROP CONSTRAINT [' + @var13 + '];');
    ALTER TABLE [Ships] DROP COLUMN [IsHorizontal];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003072949_RemovedWeaponGroups')
BEGIN
    DECLARE @var14 sysname;
    SELECT @var14 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Weapons]') AND [c].[name] = N'UserId');
    IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Weapons] DROP CONSTRAINT [' + @var14 + '];');
    ALTER TABLE [Weapons] ALTER COLUMN [UserId] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003072949_RemovedWeaponGroups')
BEGIN
    DECLARE @var15 sysname;
    SELECT @var15 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Weapons]') AND [c].[name] = N'RoomId');
    IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Weapons] DROP CONSTRAINT [' + @var15 + '];');
    ALTER TABLE [Weapons] ALTER COLUMN [RoomId] nvarchar(max) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003072949_RemovedWeaponGroups')
BEGIN
    ALTER TABLE [Weapons] ADD [MapRoomId] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003072949_RemovedWeaponGroups')
BEGIN
    ALTER TABLE [Weapons] ADD [MapUserId] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003072949_RemovedWeaponGroups')
BEGIN
    ALTER TABLE [Ships] ADD [XOffset] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003072949_RemovedWeaponGroups')
BEGIN
    ALTER TABLE [Ships] ADD [YOffset] int NOT NULL DEFAULT 0;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003072949_RemovedWeaponGroups')
BEGIN
    ALTER TABLE [Maps] ADD [EmenyShot_Y] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003072949_RemovedWeaponGroups')
BEGIN
    ALTER TABLE [Maps] ADD [EnemyShot_X] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003072949_RemovedWeaponGroups')
BEGIN
    CREATE INDEX [IX_Weapons_WeaponTypeId] ON [Weapons] ([WeaponTypeId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003072949_RemovedWeaponGroups')
BEGIN
    CREATE INDEX [IX_Weapons_MapUserId_MapRoomId] ON [Weapons] ([MapUserId], [MapRoomId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003072949_RemovedWeaponGroups')
BEGIN
    ALTER TABLE [Weapons] ADD CONSTRAINT [FK_Weapons_WeaponTypes_WeaponTypeId] FOREIGN KEY ([WeaponTypeId]) REFERENCES [WeaponTypes] ([Id]) ON DELETE CASCADE;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003072949_RemovedWeaponGroups')
BEGIN
    ALTER TABLE [Weapons] ADD CONSTRAINT [FK_Weapons_Maps_MapUserId_MapRoomId] FOREIGN KEY ([MapUserId], [MapRoomId]) REFERENCES [Maps] ([UserId], [RoomId]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003072949_RemovedWeaponGroups')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201003072949_RemovedWeaponGroups', N'3.1.7');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003110929_FixTypingInMap')
BEGIN
    DECLARE @var16 sysname;
    SELECT @var16 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Maps]') AND [c].[name] = N'EmenyShot_Y');
    IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Maps] DROP CONSTRAINT [' + @var16 + '];');
    ALTER TABLE [Maps] DROP COLUMN [EmenyShot_Y];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003110929_FixTypingInMap')
BEGIN
    ALTER TABLE [Maps] ADD [EnemyShot_Y] int NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003110929_FixTypingInMap')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201003110929_FixTypingInMap', N'3.1.7');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003180358_AddedWinnerProperty')
BEGIN
    ALTER TABLE [Rooms] ADD [WinnerId] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003180358_AddedWinnerProperty')
BEGIN
    CREATE INDEX [IX_Rooms_WinnerId] ON [Rooms] ([WinnerId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003180358_AddedWinnerProperty')
BEGIN
    ALTER TABLE [Rooms] ADD CONSTRAINT [FK_Rooms_AspNetUsers_WinnerId] FOREIGN KEY ([WinnerId]) REFERENCES [AspNetUsers] ([Id]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201003180358_AddedWinnerProperty')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201003180358_AddedWinnerProperty', N'3.1.7');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201004200039_NullableIsHostTurn')
BEGIN
    DECLARE @var17 sysname;
    SELECT @var17 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Rooms]') AND [c].[name] = N'IsHostTurn');
    IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Rooms] DROP CONSTRAINT [' + @var17 + '];');
    ALTER TABLE [Rooms] ALTER COLUMN [IsHostTurn] bit NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201004200039_NullableIsHostTurn')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201004200039_NullableIsHostTurn', N'3.1.7');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005053225_NotNullableIsHostTurn')
BEGIN
    DECLARE @var18 sysname;
    SELECT @var18 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Rooms]') AND [c].[name] = N'IsHostTurn');
    IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Rooms] DROP CONSTRAINT [' + @var18 + '];');
    ALTER TABLE [Rooms] ALTER COLUMN [IsHostTurn] bit NOT NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005053225_NotNullableIsHostTurn')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201005053225_NotNullableIsHostTurn', N'3.1.7');
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005071314_AddMapRetalationshipToWeapon')
BEGIN
    ALTER TABLE [Weapons] DROP CONSTRAINT [FK_Weapons_Maps_MapUserId_MapRoomId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005071314_AddMapRetalationshipToWeapon')
BEGIN
    DROP INDEX [IX_Weapons_MapUserId_MapRoomId] ON [Weapons];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005071314_AddMapRetalationshipToWeapon')
BEGIN
    DECLARE @var19 sysname;
    SELECT @var19 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Weapons]') AND [c].[name] = N'MapRoomId');
    IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Weapons] DROP CONSTRAINT [' + @var19 + '];');
    ALTER TABLE [Weapons] DROP COLUMN [MapRoomId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005071314_AddMapRetalationshipToWeapon')
BEGIN
    DECLARE @var20 sysname;
    SELECT @var20 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Weapons]') AND [c].[name] = N'MapUserId');
    IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [Weapons] DROP CONSTRAINT [' + @var20 + '];');
    ALTER TABLE [Weapons] DROP COLUMN [MapUserId];
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005071314_AddMapRetalationshipToWeapon')
BEGIN
    DECLARE @var21 sysname;
    SELECT @var21 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Weapons]') AND [c].[name] = N'UserId');
    IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Weapons] DROP CONSTRAINT [' + @var21 + '];');
    ALTER TABLE [Weapons] ALTER COLUMN [UserId] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005071314_AddMapRetalationshipToWeapon')
BEGIN
    DECLARE @var22 sysname;
    SELECT @var22 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Weapons]') AND [c].[name] = N'RoomId');
    IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [Weapons] DROP CONSTRAINT [' + @var22 + '];');
    ALTER TABLE [Weapons] ALTER COLUMN [RoomId] nvarchar(450) NULL;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005071314_AddMapRetalationshipToWeapon')
BEGIN
    CREATE INDEX [IX_Weapons_UserId_RoomId] ON [Weapons] ([UserId], [RoomId]);
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005071314_AddMapRetalationshipToWeapon')
BEGIN
    ALTER TABLE [Weapons] ADD CONSTRAINT [fkc_weapon_map] FOREIGN KEY ([UserId], [RoomId]) REFERENCES [Maps] ([UserId], [RoomId]) ON DELETE NO ACTION;
END;

GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20201005071314_AddMapRetalationshipToWeapon')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20201005071314_AddMapRetalationshipToWeapon', N'3.1.7');
END;

GO

