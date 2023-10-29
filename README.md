# Project .NET Framework

* Naam: Muhammet Murat
* Studentennummer: 0154865-53
* Academiejaar: 23-24
* Klasgroep: INF202A
* Onderwerp: Bedrijf 1-* Game * - * Store

## Sprint 3

### Beide zoekcriteria ingevuld
```sql

```

### Enkel zoeken op naam
```sql

```

### Enkel zoeken op openinghour
```sql
SELECT "s"."Id", "s"."Address", "s"."Name", "s"."OpeningHour"
FROM "Stores" AS "s"
WHERE "s"."OpeningHour" = @__hour_0
```

### Beide zoekcriteria leeg
```sql
SELECT "s"."Id", "s"."Address", "s"."Name", "s"."OpeningHour"
FROM "Stores" AS "s"
```