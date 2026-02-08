-> greenGuy
=== greenGuy ===
Are you the hero of legend?
+ [No]
    Yeah, that makes sense.
    (You wanna fight him but the developers are lazy.)
    (Sucks to suck.)
    -> DONE
* [Yes]
    Oh, you are?
    Can you lower my taxes...?
    (You explain how being the chosen one doesn't allow you to fight the IRS)
- -> DONE

-> dummy
=== dummy ===
Fight?
VAR inBattle = false
+ [No]
The dummys cold stare frightens you.
(You walk away slowly.)
(coward...)
-> DONE
+ [Yes]
(You prepare for battle!)
~inBattle = true
-> DONE

-> sign
=== sign ===
(You saw the sign.)
(It opened up your eyes.)
(You saw the si-ign.)
-> DONE

-> bush1
=== bush1 ===
The bush to the left is lying.
-> DONE

-> bush2
=== bush2 ===
The bush to the LEFT is dyslexic.
And a really bad liar.
(You don't know what to believe.)
(Bushception...)
-> DONE

-> churchLady
=== churchLady ===
Hello my child, I hope you are well.
I love watching the fountain flow.
It gives me hope for the future of the town.
-> DONE

-> shopDoor
=== shopDoor ===
(You notice a sign on the door.)
[-- FORECLOSURE --]
(Without a shop system in place, the store went bankrupt almost immediately.)
(Gotta love capitalism!)
-> DONE

-> bigHouseDoor
=== bigHouseDoor ===
(You went to open the door.)
(But decided not to...)
(You have a feeling these will be coming soon.)
-> DONE

-> yellowHouseDoor
=== yellowHouseDoor ===
(The door is locked.)
(Knock?)
+ [Knock]
    (You knocked.)
    GO AWAY!!!
    (How rude...)
    -> DONE
+ [Don't Knock]
    HEY...!
    WHY DIDN"T YOU KNOCK?!
    I'M VERY FRIENDLY WHEN YOU GET TO KNOW ME!
    (How sad...)
    -> DONE
    
-> oldFart
=== oldFart ===
(You want to pull his singular tuft of hair.)
+ [Don't Pull]
    (Probably for the better.)
    -> DONE
* [Pull]
    (He moans... loudly...)
    (You're disturbed.)
- -> DONE

-> fountain
=== fountain ===
It's a magestic fountain.
(The rushing water gives you the urge to pee.)
-> DONE

-> shortGuy
=== shortGuy ===
Be honest with me, am I short?
+ [You're 6'10]
    (He blushes.)
    Stawppp...
    (You have a feeling this is the first time this has happened.)
    -> DONE
* [Yes]
    (He breaks out into tears.)
    WHY WOULD YOU CALL ME SHORT!?
    I MEAN I KNOW I'M SHORT BUT C'MON!!
    (You have a feeling this isn't the first time this has happened.)
- -> DONE