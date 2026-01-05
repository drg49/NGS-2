-> start

=== start ===
"Welcome to Big Burger. What do you want?"
    + [Beef Burger, please.]
        -> orderBeef("Beef Burger. Alright. You want it plain or with the works?")
    + [Chicken Sandwich, please.]
        -> orderChicken("Chicken Sandwich. Got it. Spicy or regular?")

=== orderBeef(response) ===
{response}
    + [Plain, thanks.]
        -> beefPlain("Plain. Fine. It'll be ready. Don’t take too long.")
    + [With the works.]
        -> beefWorks("Works. Hmph. Hope you like it messy.")

=== orderChicken(response) ===
{response}
    + [Regular, thanks.]
        -> chickenRegular("Regular. Sure. Not much to say about that.")
    + [Spicy, please.]
        -> chickenSpicy("Spicy. Brave, huh? Don’t blame me if it bites.")

=== beefPlain(response) ===
{response}
    + [Thanks.]
        -> endOrder("Yeah, yeah. It'll be on the counter in a minute.")
    + [Actually, change it to with the works.]
        -> beefWorks("Finally decided? Works it is. Hurry up.")

=== beefWorks(response) ===
{response}
    + [Alright, thanks.]
        -> endOrder("Yeah, yeah. Don’t make me repeat myself.")
    + [Actually, make it plain.]
        -> beefPlain("Backtracking now? Fine, plain it is.")

=== chickenRegular(response) ===
{response}
    + [Thanks.]
        -> endOrder("Sure. It'll be ready shortly.")
    + [Actually, spicy instead.]
        -> chickenSpicy("Finally decided? Spicy it is. Don't burn your mouth.")

=== chickenSpicy(response) ===
{response}
    + [Alright, thanks.]
        -> endOrder("Good. Don’t say I didn’t warn you.")
    + [Actually, make it regular.]
        -> chickenRegular("Backtracking? Fine, regular it is.")

=== endOrder(response) ===
{response}

-> END
