-> start

=== start ===
[Me]: Hey did you guys see that?
    + [Continue]
        -> resOne("[Marcus]: Nah what are you talking about?")

=== resOne(response) ===
{response}
    + [I thought I saw something run into the woods.]
        -> resTwo("[David]: Maybe it was an animal or something. Not uncommon to see that out here...")
    + [Hmm... Nevermind.]
        -> resTwo("[David]: Hope you're not trying to scare us. It was probably just an animal.")

=== resTwo(response) ===
{response}
    + [It definitely wasn't an animal.]
        -> lastRes("[Marcus]: Okay... Well nothing is out there, so calm down. Why don't we get some sleep soon? It's getting late.")
    + [Okay. I thought it was something else...]
        -> lastRes("[Marcus]: Okay... Well let's get some sleep soon. It is getting late.")

=== lastRes(response) ===
{response}

-> END
