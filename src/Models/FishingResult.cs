using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokebot.Models
{
    public enum FishingResult
    {
        FISHING_STATE_INIT_LOCK = 0,  // Fishing1: lock des contrôles, preventStep=TRUE
        FISHING_STATE_SETUP = 1,  // Fishing2: init (rod, rounds, gfx, anim)
        FISHING_STATE_WAIT_FIRST_SECOND = 2,  // Fishing3: attendre ~1s
        FISHING_STATE_START_ROUND = 3,  // Fishing4: prépare un round de points "…"
        FISHING_STATE_PLAY_DOTS = 4,  // Fishing5: joue le "dot game"
        FISHING_STATE_CHECK_BITE = 5,  // Fishing6: détermination morsure / pas de morsure
        FISHING_STATE_GOT_BITE = 6,  // Fishing7: "Oh! A Bite!" -> saute à ON_HOOK
        FISHING_STATE_REEL_WINDOW = 7,  // Fishing8: fenêtre d'appui A
        FISHING_STATE_DECIDE_CONTINUE = 8,  // Fishing9: rejouer un round ?
        FISHING_STATE_ON_HOOK = 9,  // Fishing10: "Pokemon on the hook"
        FISHING_STATE_START_ENCOUNTER = 10, // Fishing11: lance la rencontre et détruit la task
        FISHING_STATE_NO_BITE = 11, // Fishing12: "Not even a nibble"
        FISHING_STATE_GOT_AWAY = 12, // Fishing13: "It got away"
        FISHING_STATE_SHOW_RESULT = 13, // Fishing14: petite attente d'affichage
        FISHING_STATE_WAIT_ANIM_END = 14, // Fishing15: restore gfx du joueur après anim
        FISHING_STATE_CLEANUP = 15  // Fishing16: clear UI, unlock, fin
    }
}
