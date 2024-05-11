# ProjetFinal
ProjetFinal_CollegeLasalle2024
Rapport:

Dans les taches demandees, deux n'ont pas ete termine. 

Observer: J'ai commence a faire les scripts, mais le tout n'est pas encore relie. Manque de temps pour terminer le tout.

State: J'ai le script EnnemyState relie a GrosTaDMorve, mais desactive. Quand je l'active, il tombe a 0 Hp et ne se deplace plus. Manque de temps pour fixer le probleme. 

//-------------------------------------------------------------------------------------------------------------------

Ensuite voici-voila

Classes Singleton: Monster, AudioManager, PlayerController, EnnemyFactory, EnergyBall, MiniEnergyBall, Scythe, ObjectPool(obviously), EnnemySpawner, GameManager, ExpBar.

ObjectPool : 3 Weapon du joueur dans le Pool, Scythe, EnergyWeapon et MiniEnergyWeapon

Factory: Tout les ennemy passe par le EnnemyFactory et sont divise en 3 categories: weakEnnemy, distanceEnnemy et bossEnnemy

Proxy: Les boss lors de leurs creations ont un coeur relie a eux. Les toucher leur envoie 2x le nombre de dommage

Gameplay: Les ennemies laissent des xpOrbs et goldCoin (une chance sur 3) lors de leur mort. Pour monter du niveau 1 a 2, cela prends 5 xp et ensuite incremente de 5 par niveau. A chaque niveau le player recoit: +1hp et +1hpMax, chaque arme spawner (Scythe et EnergyBall) sont spawner avec une version de plus. A tout les 10 niveau, une epee flotante apparait en plus. Les ennemies, lors de leur spawn, recoive leur hp + un modificateur selon le niveau du joueur et sont spawner a 2 exemplaire de plus par niveau du joueur(sauf Boss).

Qualite visuel: J'ai fait mes sprites et leur spriteSheet, mais egalement leur animations. Musique en arriere-fond, son de mort et d'impact. Plusieurs implementations n'ont faite par manque de temps, mais deja tres bien :)

Rapport: SSSSSSSSSAAAAAAAAAALLLLLLLLUUUUUUUUUTTTTTT je suis le rapport

GitHub: J'ai fait bon nombre de mise a niveau. La seule journee sans est hier (jeudi 9 mai). Cause Gastro: des minis. Oui c'etait vraiment aussi joyeux que vous pouvez le penser. J'ai du m'occuper d'eux alors je n'ai meme pas eu le temps d'ouvrir mon PC tellement j'etais dans la ... Voila :') 


Je tiens a vous remercier pour cette fin de session plus qu'anormal. Votre enseignement est clair et remplis d'exemple et c'est un plaisir d'assister a vos cours :) Au plaisir de se recroisser
