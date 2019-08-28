--
-- PostgreSQL database dump
--

-- Dumped from database version 11.5
-- Dumped by pg_dump version 11.5

-- Started on 2019-08-28 08:33:37

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_with_oids = false;

--
-- TOC entry 198 (class 1259 OID 16726)
-- Name: Cliente; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."Cliente" (
    "Id" integer NOT NULL,
    "Nome" character varying(50) NOT NULL,
    "CpfOuCnpj" character varying(15),
    "Tipo" integer NOT NULL,
    "DataNascimento" timestamp without time zone NOT NULL,
    "Endereco" character varying(150),
    "Score" numeric DEFAULT 0.0 NOT NULL
);


ALTER TABLE public."Cliente" OWNER TO postgres;

--
-- TOC entry 197 (class 1259 OID 16724)
-- Name: Cliente_Id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public."Cliente_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public."Cliente_Id_seq" OWNER TO postgres;

--
-- TOC entry 2854 (class 0 OID 0)
-- Dependencies: 197
-- Name: Cliente_Id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public."Cliente_Id_seq" OWNED BY public."Cliente"."Id";


--
-- TOC entry 196 (class 1259 OID 16719)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE public."__EFMigrationsHistory" OWNER TO postgres;

--
-- TOC entry 200 (class 1259 OID 16755)
-- Name: clientetelefone; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.clientetelefone (
    id integer NOT NULL,
    numero character varying(18) NOT NULL,
    clienteid integer
);


ALTER TABLE public.clientetelefone OWNER TO postgres;

--
-- TOC entry 203 (class 1259 OID 24913)
-- Name: clientecomtelefone; Type: VIEW; Schema: public; Owner: postgres
--

CREATE VIEW public.clientecomtelefone AS
 SELECT c."Id",
    c."Nome",
    c."CpfOuCnpj",
    c."Tipo",
    c."DataNascimento",
    c."Endereco",
    c."Score",
    ( SELECT t.numero
           FROM public.clientetelefone t
          WHERE (t.clienteid = c."Id")
         LIMIT 1) AS "Telefone"
   FROM public."Cliente" c;


ALTER TABLE public.clientecomtelefone OWNER TO postgres;

--
-- TOC entry 199 (class 1259 OID 16753)
-- Name: clientetelefone_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.clientetelefone_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.clientetelefone_id_seq OWNER TO postgres;

--
-- TOC entry 2855 (class 0 OID 0)
-- Dependencies: 199
-- Name: clientetelefone_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.clientetelefone_id_seq OWNED BY public.clientetelefone.id;


--
-- TOC entry 202 (class 1259 OID 24903)
-- Name: numeracao; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.numeracao (
    id integer NOT NULL,
    ultimonumero integer NOT NULL
);


ALTER TABLE public.numeracao OWNER TO postgres;

--
-- TOC entry 201 (class 1259 OID 24901)
-- Name: numeracao_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.numeracao_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE public.numeracao_id_seq OWNER TO postgres;

--
-- TOC entry 2856 (class 0 OID 0)
-- Dependencies: 201
-- Name: numeracao_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.numeracao_id_seq OWNED BY public.numeracao.id;


--
-- TOC entry 2706 (class 2604 OID 16729)
-- Name: Cliente Id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Cliente" ALTER COLUMN "Id" SET DEFAULT nextval('public."Cliente_Id_seq"'::regclass);


--
-- TOC entry 2708 (class 2604 OID 16758)
-- Name: clientetelefone id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clientetelefone ALTER COLUMN id SET DEFAULT nextval('public.clientetelefone_id_seq'::regclass);


--
-- TOC entry 2709 (class 2604 OID 24906)
-- Name: numeracao id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.numeracao ALTER COLUMN id SET DEFAULT nextval('public.numeracao_id_seq'::regclass);




--
-- TOC entry 2714 (class 2606 OID 16731)
-- Name: Cliente PK_Cliente; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."Cliente"
    ADD CONSTRAINT "PK_Cliente" PRIMARY KEY ("Id");


--
-- TOC entry 2711 (class 2606 OID 16723)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 2716 (class 2606 OID 16760)
-- Name: clientetelefone pk_clientetelefone; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clientetelefone
    ADD CONSTRAINT pk_clientetelefone PRIMARY KEY (id);


--
-- TOC entry 2718 (class 2606 OID 24908)
-- Name: numeracao pk_numeracao; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.numeracao
    ADD CONSTRAINT pk_numeracao PRIMARY KEY (id);


--
-- TOC entry 2712 (class 1259 OID 16736)
-- Name: IX_Cliente_CpfOuCnpj; Type: INDEX; Schema: public; Owner: postgres
--

CREATE UNIQUE INDEX "IX_Cliente_CpfOuCnpj" ON public."Cliente" USING btree ("CpfOuCnpj");


--
-- TOC entry 2719 (class 2606 OID 16761)
-- Name: clientetelefone fk_clientetelefone_cliente; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.clientetelefone
    ADD CONSTRAINT fk_clientetelefone_cliente FOREIGN KEY (clienteid) REFERENCES public."Cliente"("Id");


-- Completed on 2019-08-28 08:33:37

--
-- PostgreSQL database dump complete
--

------------------------------------------------------------------------


--
-- TOC entry 198 (class 1259 OID 16726)
-- Name: Cliente; Type: TABLE; Schema: teste01; Owner: postgres
--

create schema teste01;

CREATE TABLE teste01."Cliente" (
    "Id" integer NOT NULL,
    "Nome" character varying(50) NOT NULL,
    "CpfOuCnpj" character varying(15),
    "Tipo" integer NOT NULL,
    "DataNascimento" timestamp without time zone NOT NULL,
    "Endereco" character varying(150),
    "Score" numeric DEFAULT 0.0 NOT NULL
);


ALTER TABLE teste01."Cliente" OWNER TO postgres;

--
-- TOC entry 197 (class 1259 OID 16724)
-- Name: Cliente_Id_seq; Type: SEQUENCE; Schema: teste01; Owner: postgres
--

CREATE SEQUENCE teste01."Cliente_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE teste01."Cliente_Id_seq" OWNER TO postgres;

--
-- TOC entry 2854 (class 0 OID 0)
-- Dependencies: 197
-- Name: Cliente_Id_seq; Type: SEQUENCE OWNED BY; Schema: teste01; Owner: postgres
--

ALTER SEQUENCE teste01."Cliente_Id_seq" OWNED BY teste01."Cliente"."Id";


--
-- TOC entry 196 (class 1259 OID 16719)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: teste01; Owner: postgres
--

CREATE TABLE teste01."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE teste01."__EFMigrationsHistory" OWNER TO postgres;

--
-- TOC entry 200 (class 1259 OID 16755)
-- Name: clientetelefone; Type: TABLE; Schema: teste01; Owner: postgres
--

CREATE TABLE teste01.clientetelefone (
    id integer NOT NULL,
    numero character varying(18) NOT NULL,
    clienteid integer
);


ALTER TABLE teste01.clientetelefone OWNER TO postgres;

--
-- TOC entry 203 (class 1259 OID 24913)
-- Name: clientecomtelefone; Type: VIEW; Schema: teste01; Owner: postgres
--

CREATE VIEW teste01.clientecomtelefone AS
 SELECT c."Id",
    c."Nome",
    c."CpfOuCnpj",
    c."Tipo",
    c."DataNascimento",
    c."Endereco",
    c."Score",
    ( SELECT t.numero
           FROM teste01.clientetelefone t
          WHERE (t.clienteid = c."Id")
         LIMIT 1) AS "Telefone"
   FROM teste01."Cliente" c;


ALTER TABLE teste01.clientecomtelefone OWNER TO postgres;

--
-- TOC entry 199 (class 1259 OID 16753)
-- Name: clientetelefone_id_seq; Type: SEQUENCE; Schema: teste01; Owner: postgres
--

CREATE SEQUENCE teste01.clientetelefone_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE teste01.clientetelefone_id_seq OWNER TO postgres;

--
-- TOC entry 2855 (class 0 OID 0)
-- Dependencies: 199
-- Name: clientetelefone_id_seq; Type: SEQUENCE OWNED BY; Schema: teste01; Owner: postgres
--

ALTER SEQUENCE teste01.clientetelefone_id_seq OWNED BY teste01.clientetelefone.id;


--
-- TOC entry 202 (class 1259 OID 24903)
-- Name: numeracao; Type: TABLE; Schema: teste01; Owner: postgres
--

CREATE TABLE teste01.numeracao (
    id integer NOT NULL,
    ultimonumero integer NOT NULL
);


ALTER TABLE teste01.numeracao OWNER TO postgres;

--
-- TOC entry 201 (class 1259 OID 24901)
-- Name: numeracao_id_seq; Type: SEQUENCE; Schema: teste01; Owner: postgres
--

CREATE SEQUENCE teste01.numeracao_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE teste01.numeracao_id_seq OWNER TO postgres;

--
-- TOC entry 2856 (class 0 OID 0)
-- Dependencies: 201
-- Name: numeracao_id_seq; Type: SEQUENCE OWNED BY; Schema: teste01; Owner: postgres
--

ALTER SEQUENCE teste01.numeracao_id_seq OWNED BY teste01.numeracao.id;


--
-- TOC entry 2706 (class 2604 OID 16729)
-- Name: Cliente Id; Type: DEFAULT; Schema: teste01; Owner: postgres
--

ALTER TABLE ONLY teste01."Cliente" ALTER COLUMN "Id" SET DEFAULT nextval('teste01."Cliente_Id_seq"'::regclass);


--
-- TOC entry 2708 (class 2604 OID 16758)
-- Name: clientetelefone id; Type: DEFAULT; Schema: teste01; Owner: postgres
--

ALTER TABLE ONLY teste01.clientetelefone ALTER COLUMN id SET DEFAULT nextval('teste01.clientetelefone_id_seq'::regclass);


--
-- TOC entry 2709 (class 2604 OID 24906)
-- Name: numeracao id; Type: DEFAULT; Schema: teste01; Owner: postgres
--

ALTER TABLE ONLY teste01.numeracao ALTER COLUMN id SET DEFAULT nextval('teste01.numeracao_id_seq'::regclass);




--
-- TOC entry 2714 (class 2606 OID 16731)
-- Name: Cliente PK_Cliente; Type: CONSTRAINT; Schema: teste01; Owner: postgres
--

ALTER TABLE ONLY teste01."Cliente"
    ADD CONSTRAINT "PK_Cliente" PRIMARY KEY ("Id");


--
-- TOC entry 2711 (class 2606 OID 16723)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: teste01; Owner: postgres
--

ALTER TABLE ONLY teste01."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 2716 (class 2606 OID 16760)
-- Name: clientetelefone pk_clientetelefone; Type: CONSTRAINT; Schema: teste01; Owner: postgres
--

ALTER TABLE ONLY teste01.clientetelefone
    ADD CONSTRAINT pk_clientetelefone PRIMARY KEY (id);


--
-- TOC entry 2718 (class 2606 OID 24908)
-- Name: numeracao pk_numeracao; Type: CONSTRAINT; Schema: teste01; Owner: postgres
--

ALTER TABLE ONLY teste01.numeracao
    ADD CONSTRAINT pk_numeracao PRIMARY KEY (id);


--
-- TOC entry 2712 (class 1259 OID 16736)
-- Name: IX_Cliente_CpfOuCnpj; Type: INDEX; Schema: teste01; Owner: postgres
--

CREATE UNIQUE INDEX "IX_Cliente_CpfOuCnpj" ON teste01."Cliente" USING btree ("CpfOuCnpj");


--
-- TOC entry 2719 (class 2606 OID 16761)
-- Name: clientetelefone fk_clientetelefone_cliente; Type: FK CONSTRAINT; Schema: teste01; Owner: postgres
--

ALTER TABLE ONLY teste01.clientetelefone
    ADD CONSTRAINT fk_clientetelefone_cliente FOREIGN KEY (clienteid) REFERENCES teste01."Cliente"("Id");


-- Completed on 2019-08-28 08:33:37

--
-- PostgreSQL database dump complete
--

------------------------------------------------------------------------------------------

--
-- TOC entry 198 (class 1259 OID 16726)
-- Name: Cliente; Type: TABLE; Schema: teste02; Owner: postgres
--

create schema teste02;

CREATE TABLE teste02."Cliente" (
    "Id" integer NOT NULL,
    "Nome" character varying(50) NOT NULL,
    "CpfOuCnpj" character varying(15),
    "Tipo" integer NOT NULL,
    "DataNascimento" timestamp without time zone NOT NULL,
    "Endereco" character varying(150),
    "Score" numeric DEFAULT 0.0 NOT NULL
);


ALTER TABLE teste02."Cliente" OWNER TO postgres;

--
-- TOC entry 197 (class 1259 OID 16724)
-- Name: Cliente_Id_seq; Type: SEQUENCE; Schema: teste02; Owner: postgres
--

CREATE SEQUENCE teste02."Cliente_Id_seq"
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE teste02."Cliente_Id_seq" OWNER TO postgres;

--
-- TOC entry 2854 (class 0 OID 0)
-- Dependencies: 197
-- Name: Cliente_Id_seq; Type: SEQUENCE OWNED BY; Schema: teste02; Owner: postgres
--

ALTER SEQUENCE teste02."Cliente_Id_seq" OWNED BY teste02."Cliente"."Id";


--
-- TOC entry 196 (class 1259 OID 16719)
-- Name: __EFMigrationsHistory; Type: TABLE; Schema: teste02; Owner: postgres
--

CREATE TABLE teste02."__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL
);


ALTER TABLE teste02."__EFMigrationsHistory" OWNER TO postgres;

--
-- TOC entry 200 (class 1259 OID 16755)
-- Name: clientetelefone; Type: TABLE; Schema: teste02; Owner: postgres
--

CREATE TABLE teste02.clientetelefone (
    id integer NOT NULL,
    numero character varying(18) NOT NULL,
    clienteid integer
);


ALTER TABLE teste02.clientetelefone OWNER TO postgres;

--
-- TOC entry 203 (class 1259 OID 24913)
-- Name: clientecomtelefone; Type: VIEW; Schema: teste02; Owner: postgres
--

CREATE VIEW teste02.clientecomtelefone AS
 SELECT c."Id",
    c."Nome",
    c."CpfOuCnpj",
    c."Tipo",
    c."DataNascimento",
    c."Endereco",
    c."Score",
    ( SELECT t.numero
           FROM teste02.clientetelefone t
          WHERE (t.clienteid = c."Id")
         LIMIT 1) AS "Telefone"
   FROM teste02."Cliente" c;


ALTER TABLE teste02.clientecomtelefone OWNER TO postgres;

--
-- TOC entry 199 (class 1259 OID 16753)
-- Name: clientetelefone_id_seq; Type: SEQUENCE; Schema: teste02; Owner: postgres
--

CREATE SEQUENCE teste02.clientetelefone_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE teste02.clientetelefone_id_seq OWNER TO postgres;

--
-- TOC entry 2855 (class 0 OID 0)
-- Dependencies: 199
-- Name: clientetelefone_id_seq; Type: SEQUENCE OWNED BY; Schema: teste02; Owner: postgres
--

ALTER SEQUENCE teste02.clientetelefone_id_seq OWNED BY teste02.clientetelefone.id;


--
-- TOC entry 202 (class 1259 OID 24903)
-- Name: numeracao; Type: TABLE; Schema: teste02; Owner: postgres
--

CREATE TABLE teste02.numeracao (
    id integer NOT NULL,
    ultimonumero integer NOT NULL
);


ALTER TABLE teste02.numeracao OWNER TO postgres;

--
-- TOC entry 201 (class 1259 OID 24901)
-- Name: numeracao_id_seq; Type: SEQUENCE; Schema: teste02; Owner: postgres
--

CREATE SEQUENCE teste02.numeracao_id_seq
    AS integer
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER TABLE teste02.numeracao_id_seq OWNER TO postgres;

--
-- TOC entry 2856 (class 0 OID 0)
-- Dependencies: 201
-- Name: numeracao_id_seq; Type: SEQUENCE OWNED BY; Schema: teste02; Owner: postgres
--

ALTER SEQUENCE teste02.numeracao_id_seq OWNED BY teste02.numeracao.id;


--
-- TOC entry 2706 (class 2604 OID 16729)
-- Name: Cliente Id; Type: DEFAULT; Schema: teste02; Owner: postgres
--

ALTER TABLE ONLY teste02."Cliente" ALTER COLUMN "Id" SET DEFAULT nextval('teste02."Cliente_Id_seq"'::regclass);


--
-- TOC entry 2708 (class 2604 OID 16758)
-- Name: clientetelefone id; Type: DEFAULT; Schema: teste02; Owner: postgres
--

ALTER TABLE ONLY teste02.clientetelefone ALTER COLUMN id SET DEFAULT nextval('teste02.clientetelefone_id_seq'::regclass);


--
-- TOC entry 2709 (class 2604 OID 24906)
-- Name: numeracao id; Type: DEFAULT; Schema: teste02; Owner: postgres
--

ALTER TABLE ONLY teste02.numeracao ALTER COLUMN id SET DEFAULT nextval('teste02.numeracao_id_seq'::regclass);




--
-- TOC entry 2714 (class 2606 OID 16731)
-- Name: Cliente PK_Cliente; Type: CONSTRAINT; Schema: teste02; Owner: postgres
--

ALTER TABLE ONLY teste02."Cliente"
    ADD CONSTRAINT "PK_Cliente" PRIMARY KEY ("Id");


--
-- TOC entry 2711 (class 2606 OID 16723)
-- Name: __EFMigrationsHistory PK___EFMigrationsHistory; Type: CONSTRAINT; Schema: teste02; Owner: postgres
--

ALTER TABLE ONLY teste02."__EFMigrationsHistory"
    ADD CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId");


--
-- TOC entry 2716 (class 2606 OID 16760)
-- Name: clientetelefone pk_clientetelefone; Type: CONSTRAINT; Schema: teste02; Owner: postgres
--

ALTER TABLE ONLY teste02.clientetelefone
    ADD CONSTRAINT pk_clientetelefone PRIMARY KEY (id);


--
-- TOC entry 2718 (class 2606 OID 24908)
-- Name: numeracao pk_numeracao; Type: CONSTRAINT; Schema: teste02; Owner: postgres
--

ALTER TABLE ONLY teste02.numeracao
    ADD CONSTRAINT pk_numeracao PRIMARY KEY (id);


--
-- TOC entry 2712 (class 1259 OID 16736)
-- Name: IX_Cliente_CpfOuCnpj; Type: INDEX; Schema: teste02; Owner: postgres
--

CREATE UNIQUE INDEX "IX_Cliente_CpfOuCnpj" ON teste02."Cliente" USING btree ("CpfOuCnpj");


--
-- TOC entry 2719 (class 2606 OID 16761)
-- Name: clientetelefone fk_clientetelefone_cliente; Type: FK CONSTRAINT; Schema: teste02; Owner: postgres
--

ALTER TABLE ONLY teste02.clientetelefone
    ADD CONSTRAINT fk_clientetelefone_cliente FOREIGN KEY (clienteid) REFERENCES teste02."Cliente"("Id");


-- Completed on 2019-08-28 08:33:37

--
-- PostgreSQL database dump complete
--

