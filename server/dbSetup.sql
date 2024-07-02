-- Active: 1719356914340@@52.43.75.200@3306@keepr
CREATE TABLE IF NOT EXISTS accounts(
  id VARCHAR(255) NOT NULL primary key COMMENT 'primary key',
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP COMMENT 'Time Created',
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP COMMENT 'Last Update',
  name varchar(255) COMMENT 'User Name',
  email varchar(255) COMMENT 'User Email',
  picture varchar(255) COMMENT 'User Picture'
) default charset utf8mb4 COMMENT '';


CREATE TABLE IF NOT EXISTS keeps(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(1000) NOT NULL,
  img VARCHAR(1000) NOT NULL,
  views INT NOT NULL DEFAULT 0,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
)

CREATE TABLE IF NOT EXISTS vaultkeeps(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  keepId INT NOT NULL,
  creatorId VARCHAR(255) NOT NULL,
  vaultId INT NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE,
  FOREIGN KEY (vaultId) REFERENCES vaults (id) ON DELETE CASCADE,
  FOREIGN KEY (keepId) REFERENCES keeps (id) ON DELETE CASCADE
)

CREATE TABLE IF NOT EXISTS vaults(
  id INT NOT NULL PRIMARY KEY AUTO_INCREMENT,
  createdAt DATETIME DEFAULT CURRENT_TIMESTAMP,
  updatedAt DATETIME DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  name VARCHAR(255) NOT NULL,
  description VARCHAR(1000),
  img VARCHAR(1000) NOT NULL,
  isPrivate BOOLEAN DEFAULT FALSE,
  creatorId VARCHAR(255) NOT NULL,
  FOREIGN KEY (creatorId) REFERENCES accounts (id) ON DELETE CASCADE
)

DROP TABLE vaults;

SELECT
keeps.*,
COUNT(vaultkeeps.id) AS kept
from
keeps
LEFT JOIN vaultkeeps on vaultkeeps.keepId = keeps.id
GROUP BY
(keeps.id)



            INSERT INTO accounts
              (name, picture, email, id)
            VALUES
              (@Name, @Picture, @Email, @Id)

INSERT INTO keeps(
            name,
            description,
            img,
            views,
            creatorId)
                VALUES(
                    @Name,
                    @Description,
                    @Img,
                    @Views,
                    @CreatorId
                );
        SELECT
        keeps.*,
        accounts.*
        FROM keeps
        JOIN accounts ON accounts.Id = keeps.CreatorId
        WHERE keeps.Id = LAST_INSERT_ID();

                INSERT INTO
        vaults(
            name,
            description,
            img,
            isPrivate,
            creatorId
        )VALUES(
            @Name,
            @Description,
            @Img,
            @IsPrivate,
            @CreatorId
        );

        SELECT
        vaults.*,
        accounts.*
        FROM vaults
        JOIN accounts ON accounts.Id = vaults.CreatorId
        WHERE 
        vaults.Id = LAST_INSERT_ID()
        ;

INSERT INTO
        vaultkeeps(
            keepId,
            vaultId,
            creatorId
            )VALUES(
                @KeepId,
                @VaultId,
                @CreatorId
        );
        SELECT
        *
        FROM vaultkeeps
        WHERE vaultkeeps.Id = LAST_INSERT_ID();