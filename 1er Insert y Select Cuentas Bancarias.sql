INSERT INTO CuentasBancarias (Id, NumCuentaBancaria, Balance, ClienteId, EsBloqueado)
VALUES
	(NEWID(), 'ORG-123', 1000.00, NEWID(), 0),
	(NEWID(), 'DST-456', 0.00, NEWID(), 0);

SELECT * FROM CuentasBancarias;