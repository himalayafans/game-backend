create table config
(
    id    uuid primary key,
    name  varchar(50) not null unique,
    value text        not null default ''
);
create table oauth
(
    id             uuid primary key,
    account_id     uuid        not null,
    provider       varchar(50) not null,
    client_id      varchar(50) not null,
    client_account varchar(50) not null,
    client_name    varchar(50) not null
);