-> start

=== start ===
"Welcome to Big Burger. What do you want?"
    + [Beef Burger, please.]
        -> orderBeef("Beef Burger. Alright. Do you want it plain or with the works?")
    + [Chicken Sandwich, please.]
        -> orderChicken("Chicken Sandwich. Got it. Spicy or regular?")

=== orderBeef(response) ===
{response}
    + [Plain, thanks.]
        -> endOrder("Plain. Fine. It'll be ready. Don’t take too long.")
    + [With the works.]
        -> endOrder("Works. Hmph. Hope you like it messy.")

=== orderChicken(response) ===
{response}
    + [Regular, thanks.]
        -> endOrder("Regular. Sure. Not much to say about that.")
    + [Spicy, please.]
        -> endOrder("Spicy. Brave, huh? Don’t blame me if it bites.")

=== endOrder(response) ===
{response}

-> END
