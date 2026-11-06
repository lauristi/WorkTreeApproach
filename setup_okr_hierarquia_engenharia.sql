-- ============================================================================
-- PROJETO: Estudo de Estruturas Hierarquicas e Arvores Recursivas (OKR Engenharia Civil)
-- DESCRICAO: Script unificado de criacao de schema, carga de dados ficticios e 
--            exemplos de consultas de travessia (recursao / DAG).
-- ============================================================================

-- 1. CRIACAO DO BANCO DE DADOS (SE NECESSARIO)
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'DbOkrEngenharia')
BEGIN
    CREATE DATABASE DbOkrEngenharia;
END
GO

USE DbOkrEngenharia;
GO

-- 2. LIMPEZA / RECRIACAO DE OBJETOS
IF OBJECT_ID(N'dbo.BaseItemRelation', N'U') IS NOT NULL DROP TABLE dbo.BaseItemRelation;
IF OBJECT_ID(N'dbo.BaseItem', N'U') IS NOT NULL DROP TABLE dbo.BaseItem;
IF OBJECT_ID(N'dbo.ItemStatus', N'U') IS NOT NULL DROP TABLE dbo.ItemStatus;
IF OBJECT_ID(N'dbo.ItemType', N'U') IS NOT NULL DROP TABLE dbo.ItemType;
IF OBJECT_ID(N'dbo.OwnerType', N'U') IS NOT NULL DROP TABLE dbo.OwnerType;
GO

-- 3. TABELAS DE DOMINIO E METADADOS
CREATE TABLE dbo.ItemType (
    Id UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_ItemType PRIMARY KEY,
    Description NVARCHAR(255) NULL
);

CREATE TABLE dbo.OwnerType (
    Id UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_OwnerType PRIMARY KEY,
    Description NVARCHAR(255) NULL
);

CREATE TABLE dbo.ItemStatus (
    Id UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_ItemStatus PRIMARY KEY,
    Description NVARCHAR(255) NULL
);

-- 4. TABELA BASE DE ITENS (DEFINICAO / CATALOGO)
CREATE TABLE dbo.BaseItem (
    Id UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_BaseItem PRIMARY KEY,
    Name NVARCHAR(255) NULL,
    Image NVARCHAR(255) NULL,
    ItemTypeId UNIQUEIDENTIFIER NULL CONSTRAINT FK_BaseItem_ItemType REFERENCES dbo.ItemType(Id),
    OwnerTypeId UNIQUEIDENTIFIER NULL CONSTRAINT FK_BaseItem_OwnerType REFERENCES dbo.OwnerType(Id),
    OwnerId UNIQUEIDENTIFIER NULL
);

-- 5. TABELA DE RELACOES HIERARQUICAS (INSTANCIA DO NO NA ARVORE)
CREATE TABLE dbo.BaseItemRelation (
    Id UNIQUEIDENTIFIER NOT NULL CONSTRAINT PK_BaseItemRelation PRIMARY KEY,
    ParentId UNIQUEIDENTIFIER NULL CONSTRAINT FK_BaseItemRelation_Parent REFERENCES dbo.BaseItemRelation(Id),
    BaseItemId UNIQUEIDENTIFIER NULL CONSTRAINT FK_BaseItemRelation_BaseItem REFERENCES dbo.BaseItem(Id),
    Name VARCHAR(250) NULL,
    Image VARCHAR(250) NULL,
    ItemTypeId UNIQUEIDENTIFIER NULL CONSTRAINT FK_BaseItemRelation_ItemType REFERENCES dbo.ItemType(Id),
    StartDate DATETIME NOT NULL,
    EndDate DATETIME NOT NULL,
    ItemStatusId UNIQUEIDENTIFIER NOT NULL CONSTRAINT FK_BaseItemRelation_ItemStatus REFERENCES dbo.ItemStatus(Id),
    OwnerTypeId UNIQUEIDENTIFIER NOT NULL CONSTRAINT FK_BaseItemRelation_OwnerType REFERENCES dbo.OwnerType(Id),
    OwnerId UNIQUEIDENTIFIER NOT NULL,
    ItemOrder INT NOT NULL
);

-- Indices recomendados para performance de travessia recursiva
CREATE NONCLUSTERED INDEX IX_BaseItemRelation_ParentId ON dbo.BaseItemRelation(ParentId) INCLUDE (ItemOrder, Name, ItemStatusId);
CREATE NONCLUSTERED INDEX IX_BaseItemRelation_BaseItemId ON dbo.BaseItemRelation(BaseItemId);
GO

-- 6. POPULACAO DE METADADOS
DECLARE @Type_Obj UNIQUEIDENTIFIER = '11111111-0000-0000-0000-000000000001';
DECLARE @Type_KR  UNIQUEIDENTIFIER = '11111111-0000-0000-0000-000000000002';
DECLARE @Type_Act UNIQUEIDENTIFIER = '11111111-0000-0000-0000-000000000003';
DECLARE @Type_Tsk UNIQUEIDENTIFIER = '11111111-0000-0000-0000-000000000004';
DECLARE @Type_Sub UNIQUEIDENTIFIER = '11111111-0000-0000-0000-000000000005';

INSERT INTO dbo.ItemType (Id, Description) VALUES
(@Type_Obj, N'Objetivo Estratégico (OKR)'),
(@Type_KR,  N'Resultado-Chave (KR)'),
(@Type_Act, N'Plano de Ação / Frente'),
(@Type_Tsk, N'Pacote de Trabalho / Tarefa'),
(@Type_Sub, N'Inspeção / Ensaio Técnico');

DECLARE @Owner_Eng UNIQUEIDENTIFIER = '22222222-0000-0000-0000-000000000001';
DECLARE @Owner_Mst UNIQUEIDENTIFIER = '22222222-0000-0000-0000-000000000002';
DECLARE @Owner_Lab UNIQUEIDENTIFIER = '22222222-0000-0000-0000-000000000003';

INSERT INTO dbo.OwnerType (Id, Description) VALUES
(@Owner_Eng, N'Engenheiro Residente'),
(@Owner_Mst, N'Mestre de Obras'),
(@Owner_Lab, N'Laboratório de Controle Tecnológico');

DECLARE @Status_Plan UNIQUEIDENTIFIER = '33333333-0000-0000-0000-000000000001';
DECLARE @Status_Exec UNIQUEIDENTIFIER = '33333333-0000-0000-0000-000000000002';
DECLARE @Status_Cura UNIQUEIDENTIFIER = '33333333-0000-0000-0000-000000000003';
DECLARE @Status_Conc UNIQUEIDENTIFIER = '33333333-0000-0000-0000-000000000004';

INSERT INTO dbo.ItemStatus (Id, Description) VALUES
(@Status_Plan, N'Planejado'),
(@Status_Exec, N'Em Execução'),
(@Status_Cura, N'Aguardando Ensaio / Cura'),
(@Status_Conc, N'Concluído');

-- 7. ITENS DE CATALOGO (BaseItem)
-- Repare que criamos itens genericos, incluindo o item compartilhado de Rompimento de CP
DECLARE @Item_Obj1    UNIQUEIDENTIFIER = 'A0000000-0000-0000-0000-000000000001';
DECLARE @Item_KR1     UNIQUEIDENTIFIER = 'A0000000-0000-0000-0000-000000000002';
DECLARE @Item_KR2     UNIQUEIDENTIFIER = 'A0000000-0000-0000-0000-000000000003';
DECLARE @Item_ActFund UNIQUEIDENTIFIER = 'A0000000-0000-0000-0000-000000000004';
DECLARE @Item_ActEstr UNIQUEIDENTIFIER = 'A0000000-0000-0000-0000-000000000005';
DECLARE @Item_TskBloc UNIQUEIDENTIFIER = 'A0000000-0000-0000-0000-000000000006';
DECLARE @Item_TskLaje UNIQUEIDENTIFIER = 'A0000000-0000-0000-0000-000000000007';
DECLARE @Item_Shared_Ensaio UNIQUEIDENTIFIER = 'A0000000-0000-0000-0000-000000000099'; -- Item compartilhado!

INSERT INTO dbo.BaseItem (Id, Name, Image, ItemTypeId, OwnerTypeId, OwnerId) VALUES
(@Item_Obj1,    N'Entregar Estrutura de Concreto Armado do Edifício Residencial', NULL, @Type_Obj, @Owner_Eng, '55555555-0000-0000-0000-000000000001'),
(@Item_KR1,     N'Concluir 100% da Fundação Profunda e Blocos de Coroamento',   NULL, @Type_KR,  @Owner_Eng, '55555555-0000-0000-0000-000000000001'),
(@Item_KR2,     N'Executar Ciclo de Concretagem das Lajes Tipo (1 a 12)',        NULL, @Type_KR,  @Owner_Eng, '55555555-0000-0000-0000-000000000001'),
(@Item_ActFund, N'Frente de Infraestrutura e Fundações',                        NULL, @Type_Act, @Owner_Mst, '55555555-0000-0000-0000-000000000002'),
(@Item_ActEstr, N'Frente de Superestrutura e Pavimentos-Tipo',                  NULL, @Type_Act, @Owner_Mst, '55555555-0000-0000-0000-000000000002'),
(@Item_TskBloc, N'Concretagem dos Blocos de Fundação',                          NULL, @Type_Tsk, @Owner_Mst, '55555555-0000-0000-0000-000000000002'),
(@Item_TskLaje, N'Armação e Concretagem da Laje do 1º Pavimento',               NULL, @Type_Tsk, @Owner_Mst, '55555555-0000-0000-0000-000000000002'),
(@Item_Shared_Ensaio, N'Controle Tecnológico e Ensaio de Rompimento de Corpos de Prova (NBR 5739)', NULL, @Type_Sub, @Owner_Lab, '55555555-0000-0000-0000-000000000003');

-- 8. MONTAGEM DAS HIERARQUIAS (BaseItemRelation)
-- Aqui os nos sao vinculados via ParentId, demonstrando a arvore e o reuso do item compartilhado
DECLARE @Rel_Root_Obj    UNIQUEIDENTIFIER = 'B0000000-0000-0000-0000-000000000001';
DECLARE @Rel_KR1         UNIQUEIDENTIFIER = 'B0000000-0000-0000-0000-000000000002';
DECLARE @Rel_KR2         UNIQUEIDENTIFIER = 'B0000000-0000-0000-0000-000000000003';
DECLARE @Rel_ActFund     UNIQUEIDENTIFIER = 'B0000000-0000-0000-0000-000000000004';
DECLARE @Rel_ActEstr     UNIQUEIDENTIFIER = 'B0000000-0000-0000-0000-000000000005';
DECLARE @Rel_TskBloc     UNIQUEIDENTIFIER = 'B0000000-0000-0000-0000-000000000006';
DECLARE @Rel_TskLaje     UNIQUEIDENTIFIER = 'B0000000-0000-0000-0000-000000000007';

-- Duas instancias do mesmo item no catalogo (Item_Shared_Ensaio) em galhos diferentes da arvore:
DECLARE @Rel_SubEnsaio_Fund UNIQUEIDENTIFIER = 'B0000000-0000-0000-0000-000000000010';
DECLARE @Rel_SubEnsaio_Laje UNIQUEIDENTIFIER = 'B0000000-0000-0000-0000-000000000020';

INSERT INTO dbo.BaseItemRelation 
    (Id, ParentId, BaseItemId, Name, Image, ItemTypeId, StartDate, EndDate, ItemStatusId, OwnerTypeId, OwnerId, ItemOrder)
VALUES
-- Nivel 1: Raiz (Objetivo Estrategico)
(@Rel_Root_Obj, NULL, @Item_Obj1, 'Objetivo Geral da Obra', NULL, @Type_Obj, '2026-01-10', '2026-12-20', @Status_Exec, @Owner_Eng, '55555555-0000-0000-0000-000000000001', 1),

-- Nivel 2: Resultados-Chave (Filhos do Objetivo Geral)
(@Rel_KR1, @Rel_Root_Obj, @Item_KR1, 'KR 01 - Fundacoes Concluidas', NULL, @Type_KR, '2026-01-15', '2026-04-30', @Status_Conc, @Owner_Eng, '55555555-0000-0000-0000-000000000001', 1),
(@Rel_KR2, @Rel_Root_Obj, @Item_KR2, 'KR 02 - Pavimentos Concretados', NULL, @Type_KR, '2026-05-01', '2026-11-30', @Status_Exec, @Owner_Eng, '55555555-0000-0000-0000-000000000001', 2),

-- Nivel 3: Planos de Acao (Filhos dos KRs correspondentes)
(@Rel_ActFund, @Rel_KR1, @Item_ActFund, 'Frente de Infraestrutura', NULL, @Type_Act, '2026-01-20', '2026-04-15', @Status_Conc, @Owner_Mst, '55555555-0000-0000-0000-000000000002', 1),
(@Rel_ActEstr, @Rel_KR2, @Item_ActEstr, 'Frente de Lajes e Pilares',  NULL, @Type_Act, '2026-05-01', '2026-11-15', @Status_Exec, @Owner_Mst, '55555555-0000-0000-0000-000000000002', 1),

-- Nivel 4: Tarefas Executivas (Filhos dos Planos de Acao)
(@Rel_TskBloc, @Rel_ActFund, @Item_TskBloc, 'Concretagem dos Blocos', NULL, @Type_Tsk, '2026-02-01', '2026-03-10', @Status_Conc, @Owner_Mst, '55555555-0000-0000-0000-000000000002', 1),
(@Rel_TskLaje, @Rel_ActEstr, @Item_TskLaje, 'Concretagem Laje Pav. 1', NULL, @Type_Tsk, '2026-05-10', '2026-05-25', @Status_Exec, @Owner_Mst, '55555555-0000-0000-0000-000000000002', 1),

-- Nivel 5: Subtarefas / Ensaios Tecnologicos (Reuso do mesmo BaseItemId em diferentes contextos)
(@Rel_SubEnsaio_Fund, @Rel_TskBloc, @Item_Shared_Ensaio, 'Rompimento CP - Concreto dos Blocos (28 dias)', NULL, @Type_Sub, '2026-02-05', '2026-03-05', @Status_Conc, @Owner_Lab, '55555555-0000-0000-0000-000000000003', 1),
(@Rel_SubEnsaio_Laje, @Rel_TskLaje, @Item_Shared_Ensaio, 'Rompimento CP - Concreto da Laje (7/28 dias)',     NULL, @Type_Sub, '2026-05-15', '2026-06-12', @Status_Cura, @Owner_Lab, '55555555-0000-0000-0000-000000000003', 1);
GO

-- ============================================================================
-- 9. CONSULTAS DEMONSTRATIVAS DE TRAVESSIA (CTE RECURSIVA)
-- ============================================================================

-- CASO A: ARVORE COMPLETA COM INDENTACAO E FORMACAO DE HIERARQUIA
WITH TreeCTE AS (
    -- No raiz
    SELECT 
        r.Id,
        r.ParentId,
        r.Name,
        t.Description AS Tipo,
        s.Description AS StatusDesc,
        r.ItemOrder,
        0 AS Nivel,
        CAST(r.Name AS NVARCHAR(MAX)) AS HierarquiaFormatada,
        CAST(RIGHT('000' + CAST(r.ItemOrder AS VARCHAR(3)), 3) AS VARCHAR(MAX)) AS OrdenacaoCaminho
    FROM dbo.BaseItemRelation r
    INNER JOIN dbo.ItemType t ON r.ItemTypeId = t.Id
    INNER JOIN dbo.ItemStatus s ON r.ItemStatusId = s.Id
    WHERE r.ParentId IS NULL

    UNION ALL

    -- Recursao para filhos
    SELECT 
        filho.Id,
        filho.ParentId,
        filho.Name,
        t.Description AS Tipo,
        s.Description AS StatusDesc,
        filho.ItemOrder,
        pai.Nivel + 1,
        CAST(REPLICATE('   ', pai.Nivel + 1) + '|-- ' + filho.Name AS NVARCHAR(MAX)),
        CAST(pai.OrdenacaoCaminho + '.' + RIGHT('000' + CAST(filho.ItemOrder AS VARCHAR(3)), 3) AS VARCHAR(MAX))
    FROM dbo.BaseItemRelation filho
    INNER JOIN TreeCTE pai ON filho.ParentId = pai.Id
    INNER JOIN dbo.ItemType t ON filho.ItemTypeId = t.Id
    INNER JOIN dbo.ItemStatus s ON filho.ItemStatusId = s.Id
)
SELECT 
    Nivel,
    HierarquiaFormatada AS Estrutura,
    Tipo,
    StatusDesc
FROM TreeCTE
ORDER BY OrdenacaoCaminho;
GO

-- CASO B: ASCENDENCIA DIRETA A PARTIR DE UM ITEM ESPECIFICO (ex.: Rompimento de CP da Laje)
DECLARE @TargetRelationId UNIQUEIDENTIFIER = 'B0000000-0000-0000-0000-000000000020';

WITH AncestorCTE AS (
    SELECT 
        r.Id,
        r.ParentId,
        r.Name,
        t.Description AS Tipo,
        0 AS Distancia
    FROM dbo.BaseItemRelation r
    INNER JOIN dbo.ItemType t ON r.ItemTypeId = t.Id
    WHERE r.Id = @TargetRelationId

    UNION ALL

    SELECT 
        pai.Id,
        pai.ParentId,
        pai.Name,
        t.Description AS Tipo,
        filho.Distancia + 1
    FROM dbo.BaseItemRelation pai
    INNER JOIN AncestorCTE filho ON pai.Id = filho.ParentId
    INNER JOIN dbo.ItemType t ON pai.ItemTypeId = t.Id
)
SELECT 
    Distancia AS GrausAcima,
    Name AS Item,
    Tipo
FROM AncestorCTE
ORDER BY Distancia ASC;
GO
