/*
 Navicat Premium Data Transfer

 Source Server         : PGlocal
 Source Server Type    : PostgreSQL
 Source Server Version : 110005
 Source Host           : localhost:5432
 Source Catalog        : votador
 Source Schema         : public

 Target Server Type    : PostgreSQL
 Target Server Version : 110005
 File Encoding         : 65001

 Date: 16/09/2019 13:18:07
*/


-- ----------------------------
-- Sequence structure for recursos_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."recursos_id_seq";
CREATE SEQUENCE "public"."recursos_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for usuarios_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."usuarios_id_seq";
CREATE SEQUENCE "public"."usuarios_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Sequence structure for votos_id_seq
-- ----------------------------
DROP SEQUENCE IF EXISTS "public"."votos_id_seq";
CREATE SEQUENCE "public"."votos_id_seq" 
INCREMENT 1
MINVALUE  1
MAXVALUE 2147483647
START 1
CACHE 1;

-- ----------------------------
-- Table structure for recursos
-- ----------------------------
DROP TABLE IF EXISTS "public"."recursos";
CREATE TABLE "public"."recursos" (
  "id" int4 NOT NULL DEFAULT nextval('recursos_id_seq'::regclass),
  "descricao" text COLLATE "pg_catalog"."default" NOT NULL
)
;

-- ----------------------------
-- Table structure for usuarios
-- ----------------------------
DROP TABLE IF EXISTS "public"."usuarios";
CREATE TABLE "public"."usuarios" (
  "id" int4 NOT NULL DEFAULT nextval('usuarios_id_seq'::regclass),
  "nome" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "email" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "senha" varchar(255) COLLATE "pg_catalog"."default" NOT NULL,
  "perfil" int4 NOT NULL
)
;

-- ----------------------------
-- Records of usuarios
-- ----------------------------
INSERT INTO "public"."usuarios" VALUES (1, 'Alterdata DP', 'dp@alterdata.com.br', 'b703d17c462b842fd4d9e1dab4a5d5927f1c20c75c38b951ddefb49ede623abc', 1);

-- ----------------------------
-- Table structure for votos
-- ----------------------------
DROP TABLE IF EXISTS "public"."votos";
CREATE TABLE "public"."votos" (
  "id" int4 NOT NULL DEFAULT nextval('votos_id_seq'::regclass),
  "usuario_id" int4 NOT NULL,
  "recurso_id" int4 NOT NULL,
  "comentario" text COLLATE "pg_catalog"."default" NOT NULL,
  "created_at" timestamp(6)
)
;

-- ----------------------------
-- Alter sequences owned by
-- ----------------------------
ALTER SEQUENCE "public"."recursos_id_seq"
OWNED BY "public"."recursos"."id";
SELECT setval('"public"."recursos_id_seq"', 8, true);
ALTER SEQUENCE "public"."usuarios_id_seq"
OWNED BY "public"."usuarios"."id";
SELECT setval('"public"."usuarios_id_seq"', 6, true);
ALTER SEQUENCE "public"."votos_id_seq"
OWNED BY "public"."votos"."id";
SELECT setval('"public"."votos_id_seq"', 7, true);

-- ----------------------------
-- Primary Key structure for table recursos
-- ----------------------------
ALTER TABLE "public"."recursos" ADD CONSTRAINT "recursos_pkey" PRIMARY KEY ("id");

-- ----------------------------
-- Primary Key structure for table usuarios
-- ----------------------------
ALTER TABLE "public"."usuarios" ADD CONSTRAINT "usuarios_pkey" PRIMARY KEY ("id");

-- ----------------------------
-- Primary Key structure for table votos
-- ----------------------------
ALTER TABLE "public"."votos" ADD CONSTRAINT "votos_pkey" PRIMARY KEY ("id");

-- ----------------------------
-- Foreign Keys structure for table votos
-- ----------------------------
ALTER TABLE "public"."votos" ADD CONSTRAINT "votos_recursos_fk" FOREIGN KEY ("recurso_id") REFERENCES "public"."recursos" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
ALTER TABLE "public"."votos" ADD CONSTRAINT "votos_usuarios_fk" FOREIGN KEY ("usuario_id") REFERENCES "public"."usuarios" ("id") ON DELETE NO ACTION ON UPDATE NO ACTION;
