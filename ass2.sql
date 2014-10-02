



-- ########################################################## --
-- ######################### Tables ######################### --
-- ########################################################## --

-- ####################### --
-- ########## 1 ########## --
-- ####################### --

CREATE TABLE [dbo].[CourseTemplates] (
    [CourseID]    NVARCHAR (64)  NOT NULL,
    [Name]        NVARCHAR (MAX) NOT NULL,
    [Description] NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([CourseID] ASC)
);

CREATE TABLE [dbo].[Semesters] (
    [ID]         NVARCHAR (6)   NOT NULL,
    [Name]       NVARCHAR (MAX) NOT NULL,
    [DateBegins] DATE           NOT NULL,
    [DateEnds]   DATE           NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);



-- ####################### --
-- ########## 2 ########## --
-- ####################### --

CREATE TABLE [dbo].[CourseInstances] (
    [ID]         INT           IDENTITY (1, 1) NOT NULL,
    [CourseID]   NVARCHAR (64) NOT NULL,
    [SemesterID] NVARCHAR (6)  NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_CourseInstances_CourseTemplates] FOREIGN KEY ([CourseID]) REFERENCES [dbo].[CourseTemplates] ([CourseID]),
    CONSTRAINT [FK_CourseInstances_Semester] FOREIGN KEY ([SemesterID]) REFERENCES [dbo].[Semesters] ([ID])
);

CREATE TABLE [dbo].[ProjectGroups] (
    [ID]                 INT            IDENTITY (1, 1) NOT NULL,
    [Name]               NVARCHAR (MAX) NOT NULL,
    [GradedProjectCount] INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC)
);



-- ####################### --
-- ########## 3 ########## --
-- ####################### --

CREATE TABLE [dbo].[Projects] (
    [ID]                        INT            IDENTITY (1, 1) NOT NULL,
    [Name]                      NVARCHAR (MAX) NOT NULL,
    [ProjectType]               INT            NOT NULL,
    [Weight]                    INT            NOT NULL,
    [CourseInstanceID]          INT            NOT NULL,
    [ProjectGroupID]            INT            NULL,
    [OnlyIfHigherThanProjectID] INT            NULL,
    [MinGradeToPassCourse]      INT            NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_Projects_CourseInstance] FOREIGN KEY ([CourseInstanceID]) REFERENCES [dbo].[CourseInstances] ([ID]),
    CONSTRAINT [FK_Projects_ProjectGroup] FOREIGN KEY ([ProjectGroupID]) REFERENCES [dbo].[ProjectGroups] ([ID]),
    CONSTRAINT [FK_Projects_Projects] FOREIGN KEY ([OnlyIfHigherThanProjectID]) REFERENCES [dbo].[Projects] ([ID])
);

CREATE TABLE [dbo].[Persons] (
    [ID]    INT            IDENTITY (1, 1) NOT NULL,
    [SSN]   NVARCHAR (10)  NOT NULL,
    [Name]  NVARCHAR (MAX) NOT NULL,
    [Email] NVARCHAR (MAX) NULL,
    PRIMARY KEY CLUSTERED ([SSN] ASC)
);



-- ####################### --
-- ########## 4 ########## --
-- ####################### --

CREATE TABLE [dbo].[Grades] (
    [ID]           INT             IDENTITY (1, 1) NOT NULL,
    [SSN]          NVARCHAR (10)   NOT NULL,
    [ProjectID]    INT             NOT NULL,
    [ProjectGrade] DECIMAL (18, 1) NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    UNIQUE NONCLUSTERED ([SSN] ASC, [ProjectID] ASC),
    CONSTRAINT [FK_Grades_Student] FOREIGN KEY ([SSN]) REFERENCES [dbo].[Persons] ([SSN]),
    CONSTRAINT [FK_Grades_Project] FOREIGN KEY ([ProjectID]) REFERENCES [dbo].[Projects] ([ID])
);

CREATE TABLE [dbo].[StudentRegistrations] (
    [ID]               INT           IDENTITY (1, 1) NOT NULL,
    [SSN]              NVARCHAR (10) NOT NULL,
    [CourseInstanceID] INT           NOT NULL,
    [Status]           INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_StudentRegistrations_Student] FOREIGN KEY ([SSN]) REFERENCES [dbo].[Persons] ([SSN]),
    CONSTRAINT [FK_StudentRegistrations_ToTable] FOREIGN KEY ([CourseInstanceID]) REFERENCES [dbo].[CourseInstances] ([ID])
);

CREATE TABLE [dbo].[TeacherRegistrations] (
    [ID]               INT           IDENTITY (1, 1) NOT NULL,
    [SSN]              NVARCHAR (10) NOT NULL,
    [CourseInstanceID] INT           NOT NULL,
    [Type]             INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_TeacherRegistrations_Teacher] FOREIGN KEY ([SSN]) REFERENCES [dbo].[Persons] ([SSN]),
    CONSTRAINT [FK_TeacherRegistrations_ToTable] FOREIGN KEY ([CourseInstanceID]) REFERENCES [dbo].[CourseInstances] ([ID])
);


-- ######################################################## --
-- ######################### Data ######################### --
-- ######################################################## --


INSERT INTO [dbo].[CourseTemplates](ID, CourseID, SemesterID) VALUES(1, 'T-514-VEFT', '20143')
INSERT INTO [dbo].[CourseTemplates](ID, CourseID, SemesterID) VALUES(2, 'T-213-VEFF', '20141')
INSERT INTO [dbo].[CourseTemplates](ID, CourseID, SemesterID) VALUES(3, 'T-111-PROG', '20143')








