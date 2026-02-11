-> start

=== start ===
[Derek]: What the hell was that?
    + [I saw something running into the woods.]
        -> resOne("[David]: I saw something in the corner of my eyes. Was it an animal or something?")
    + [I didn't see anything]
        -> resOne("[David]: I heard an animal or something.")

=== resOne(response) ===
{response}
    + [Continue]
        -> lastRes("[David]: Let's get out the hot dogs and some wine, it's dinner time!")

=== lastRes(response) ===
{response}       

-> END
