CREATE TABLE web.Users
(
	id					BIGINT					PRIMARY KEY 	IDENTITY(1,1),
	user_name				VARCHAR(35)		NOT NULL	UNIQUE,
	email					VARCHAR(320)		NOT NULL	UNIQUE,
	password_hash				VARCHAR(128)		NOT NULL,
	password_salt				VARCHAR(128)		NOT NULL,
	password_recovery_token			VARCHAR(30)				UNIQUE,
	RegistrationToken			VARCHAR(255)  ,
	attempts_count				TINYINT			NOT NULL	DEFAULT(0)	CHECK(attempts_count>=0),
	blocked					BIT			NOT NULL	DEFAULT(0),
	deleted					BIT			NOT NULL	DEFAULT(0)
);
CREATE TABLE web.Roles
(
	Id			BIGINT			PRIMARY KEY IDENTITY(1,1),
	Role			VARCHAR(30)		NOT NULL,
	Description		VARCHAR(158)		DEFAULT('Your permissions'),
	UserId			BIGINT			NOT NULL,
	CONSTRAINT FK_Roles_UserId FOREIGN KEY (UserId) REFERENCES web.Users(Id) ON DELETE CASCADE ON UPDATE CASCADE
)
CREATE TABLE web.Gallery
(
	id			BIGINT			PRIMARY KEY IDENTITY(1,1),
	title			VARCHAR(60)		NOT NULL,
	description		VARCHAR(158)		DEFAULT('Unknown description'),
	user_id			BIGINT			NOT NULL,
	FOREIGN KEY (user_id)  REFERENCES web.Users(id)  ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE TABLE web.Images
(
	id			BIGINT					PRIMARY KEY 	IDENTITY(1,1),
	title			VARCHAR(35)		NOT NULL,
	image			VARCHAR(MAX)		NOT NULL,
	user_id			BIGINT			NOT NULL,
	Likes			INT			NOT NULL	DEFAULT(0) 	CHECK(Likes>=0),
	Views			INT			NOT NULL	DEFAULT(0) 	CHECK(Views>=0),
	Check(Likes<=Views)
	FOREIGN KEY (user_id) REFERENCES web.Users(id) ON DELETE CASCADE ON UPDATE CASCADE
);
CREATE TABLE web.GalleryImages
(
	gallery_id		BIGINT		CHECK(gallery_id>=0),
	image_id		BIGINT		CHECK(image_id>=0),
	FOREIGN KEY(gallery_id) REFERENCES web.Gallery(id)  ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY(image_id)   REFERENCES web.Images(id)		,
	PRIMARY KEY(gallery_id,image_id)
)
CREATE TABLE web.Comments
(
	id			BIGINT			PRIMARY KEY		IDENTITY(1,1),
	comment			VARCHAR(8000),
	image_id		BIGINT			NOT NULL		CHECK(image_id>0)
	FOREIGN KEY(image_id) REFERENCES web.Images(id) ON DELETE CASCADE ON UPDATE CASCADE
)
CREATE TABLE web.Tags
(
	Id				BIGINT			PRIMARY KEY IDENTITY(1,1),
	Tag				VARCHAR(25)		NOT NULL,
	ImageId				BIGINT			NOT NULL,
	CONSTRAINT FK_Tags_ImageId FOREIGN KEY (ImageId) REFERENCES web.Images(Id) ON DELETE CASCADE ON UPDATE CASCADE
)
CREATE TABLE web.Categories
(
	Id			BIGINT			PRIMARY KEY IDENTITY(1,1),
	Category		VARCHAR(25)		NOT NULL,
	Title			VARCHAR(60)		NOT NULL
)
