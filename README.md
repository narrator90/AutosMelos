# AutosMotyó

## A rendszer BlazorWebApp-ot használ és .net 8 alatt fut.

## Konfigurációs fájl megnyitása
- **ConnectionString beállítása**: A megfelelő adatbázis kapcsolatának konfigurálása.
- **AutoSeed beállítás**:
  - Ha `true`, a rendszer automatikusan létrehozza az adatbázist és feltölti az alap adatokat.
  - Ha `false`, manuálisan kell feltölteni az adatbázist a `.sql` fájl segítségével.

## AutoSeed működése
- Alapvetően az AutoSeed migrálja az adatbázist, és ha nincs adat a táblákban, akkor hozzáadja az alapértelmezett adatokat.

## Alapvető felhasználók és jogosultságok
- **Felhasználók**:
  - `admin / 123456aA! / Administrator`
  - `user / 123456aA! / User`
  
- Az adminisztrátor által létrehozott felhasználók alapértelmezett jelszava: `123456aA!`.

## Jelszó policy
- A jelszó szabályzatához kapcsolódó beállítások az `appsettings.json` fájlban találhatók.

## Jogosultságkezelés
- Minden bejelentkezett felhasználó alapértelmezetten "User" szerepkört kap, így erre külön Role nem szükséges.

## AutoSeed futtatása
- Az AutoSeed minden alkalommal lefut, amikor a webalkalmazás elindul. Ha az adatbázis naprakész, vagy már van adat a táblákban, a folyamat kihagyásra kerül.
