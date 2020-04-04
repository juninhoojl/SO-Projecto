# BASE dados GameSO

DROP DATABASE IF EXISTS GameSO;
CREATE DATABASE GameSO;
USE GameSO;

CREATE TABLE Player(
	Username VARCHAR(25) NOT NULL PRIMARY KEY,
	Password CHAR(32) NOT NULL
);

CREATE TABLE Game(
	ID INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	Inicio TIMESTAMP NOT NULL DEFAULT NOW(),
	Fim TIMESTAMP NOT NULL DEFAULT NOW(),
	Vencedor VARCHAR(25),
	FOREIGN KEY (Vencedor) REFERENCES Player(Username)
		ON DELETE SET NULL ON UPDATE CASCADE
)AUTO_INCREMENT = 1;

CREATE TABLE Relaciona(
	ID INT NOT NULL AUTO_INCREMENT PRIMARY KEY,
	Game INT NOT NULL,
	Player VARCHAR(25),
	Score INTEGER NOT NULL DEFAULT 0,
	FOREIGN KEY (Player) REFERENCES Player(Username)
		ON DELETE CASCADE ON UPDATE CASCADE,
	FOREIGN KEY (Game) REFERENCES Game(ID)
		ON DELETE CASCADE ON UPDATE CASCADE
)AUTO_INCREMENT = 1;

INSERT INTO Player (Username, Password) VALUES ('jose', 'asdfghjk');
INSERT INTO Player (Username, Password) VALUES ('luiz', 'senha123');

INSERT INTO Game () VALUES ();

INSERT INTO Relaciona (Game,Player) VALUES (1,'jose');
INSERT INTO Relaciona (Game,Player) VALUES (1,'luiz');

-- # UPDATE Game Set Vencedor='jose' WHERE ID=1;

-- # SELECT * FROM Player;
-- # SELECT * FROM Game;
-- # SELECT * FROM Relaciona;

-- # DELETE FROM Player WHERE Username='Jose';

-- # SELECT * FROM Player;
-- # SELECT * FROM Game;
-- # SELECT * FROM Relaciona;
