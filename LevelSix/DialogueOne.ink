-> start

=== start ===
[David]: Well, he's breathing still... One of us needs to leave here early morning to grab the car.
    + [I can go.]
        -> resOne("[David]: Great, there should be a backroad that takes you a bit closer to the trail we're on.")
    + [How about you go? I'll stay here and make sure he's okay.]
        -> resOne("[David]: That's fine. I should be able to find my way back during daylight.")

=== resOne(response) ===
{response}
    + [So what now?]
        -> lastRes("[David]: We should find something to eat. There's food in the cupboard, but it looks expired. The good news is... there's a rifle on the wall with ammo in it. I also found some bullets in the dresser. There's a bunch of rabbits outside if you want to eat something fresh. I can butcher them if you go hunt.")

=== lastRes(response) ===
{response}  

-> END
