-> start

=== start ===
[Me]: Marcus! Wake up dude. David just ran into the woods and he's acting all crazy. I think something is wrong with him.
    + [Continue]
        -> resOne("Woah, woah... Relax man. What's going on with you?")

=== resOne(response) ===
{response}
    + [I don't know, but I think someone is onto us out here. I keep seeing strange things.]
        -> resTwo("Okay first of all, calm down... What's wrong with David?")
    + [Nothing is wrong with me! David has ran off and I think someone else is out here.]
        -> lastRes("What are you talking about dude? David is in his tent sleeping...")

=== resTwo(response) ===
{response}
    + [He's out in the woods acting manic.]
        -> lastRes("What are you talking about dude? David is in his tent sleeping...")

=== lastRes(response) ===
{response}

-> END
