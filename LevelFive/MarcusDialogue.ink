-> start

=== start ===
Hey, thanks for gathering those logs. We'll definitely have a lot to burn tonight.
    + [Sure, no problem.]
        -> resOne("So, I wonder what caused those two women to go missing around this area. Did you see their missing posters on the table over there?")
    + [Yeah, I could've used some help.]
        -> resTwo("Sorry. I was too busy organizing my tent.")

=== resOne(response) ===
{response}
    + [Yeah, I'm not sure. It happened pretty recently.]
        -> resThree("It's pretty easy to get lost out here. They probably took a wrong turn somewhere... never to be seen again.")
    + [I didn't see the poster.]
        -> resThree("You should go check it out. It's the kind of thing nightmares are made of. I imagine they got lost out here and were never seen again.")

=== resTwo(response) ===
{response}
    + [It's fine.]
        -> resOne("So, I wonder what caused those two women to go missing around this area. Did you see their missing posters on the table over there?")

=== resThree(response) ===
{response}
    + [I think something more sinister happened to them.]
        -> resEnd("That's possible. There have been stories of a hermit living out here, stealing stuff from nearby cabins. But it's probably just a myth... Anyway, it's getting dark. Let's get that fire going.")
    + [No more negativity. Let's just enjoy the trip.]
        -> resEnd("Sure. I just keep thinking about that story. Anyways, it's getting dark. Let's get that fire going.")

=== resEnd(response) ===
{response}

-> END
