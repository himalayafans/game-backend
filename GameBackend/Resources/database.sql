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
create table account
(
    id              uuid primary key,
    name            varchar(50)  not null unique,
    email           varchar(50) unique,
    password        varchar(50)  not null,
    avatar          varchar(100) not null,
    is_active_email bool         not null default false,
    last_updated    timestamp    not null default current_timestamp,
    status          smallint     not null default 0,
    create_time     timestamp    not null default current_timestamp
);