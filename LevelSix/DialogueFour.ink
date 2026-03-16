-> start

=== start ===
What? Did you call my name? It looks like the power went out. We'll need to use the wood stove until we leave here tomorrow morning.
    + [What the hell man!]
        -> resOne("What? What's going on?")
    + [Where did you go? Weren't you just outside]
        -> resTwo("No dude, I went to use the restroom. I didn't go outside at all.")

=== resOne(response) ===
{response}
    + [You were just standing outside... How did you get in here so fast?]
        -> resTwo("Huh? No dude I was using the restroom, I didn't go outside at all.")

=== resTwo(response) ===
{response}
    + [But... I just.. saw you...]
        -> resThree("You saw something outside? Should we go check?")
    + [Uh no. I definitely saw you outside.]
        -> resThree("You saw something outside? Should we go check?")

=== resThree(response) ===
{response}
    + [I'll go, you stay here.]
        -> lastRes("Alright, take a flashlight with you.")

=== lastRes(response) ===
{response}

-> END
