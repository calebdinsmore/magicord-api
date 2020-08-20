LOAD DATABASE
FROM mysql://user:password@localhost/db
INTO postgresql://magicord:magicord@localhost/magicord

WITH snake_case identifiers
;