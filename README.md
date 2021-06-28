# 4TeamCorpTest


Task: reikia sukurti web applikacija (asp.net core) kuri tures 2 endpoint`us: 
pirmas - irasys atnaujintus kliento duomenis i cache
antras - grazina tuos domenis, jeigu duomenu nera cache, tai juos reikia sugeneruoti ir irasyti, o tik po to grazinti
Endpoint`ai gauna tik viena parametra UserId.
Duomenis generuojami random budu(real life: pakraunami is DB ir apskaiciuojami). 
Duomenis turi buti sauguomi i cache ir turi tureti galiojimo laika.
Duomenu tipas - any custom class.
Cache - gali buti nauduojamas bet kuris, taciau idealus variantas Redis.
