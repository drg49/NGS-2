-> start

=== start ===
"Hello? This is Marcus."
    + [Who is this?]
        -> whoIsThis("It's me, your friend. Are you coming to the party?")
    + [Hello Marcus.]
        -> helloMarcus("Hey! Good to hear from you.")

=== whoIsThis(response) ===
{response}
    + [Yes, I'll be there.]
        -> last("Great! See you there.")
    + [No, I can't make it.]
        -> last("Oh, that's a shame. Maybe next time.")

=== helloMarcus(response) ===
{response}
    + [What's up?]
        -> last("Just checking in. Wanted to see if you got my message.")
    + [Not much.]
        -> last("Alright, talk to you later.")

=== last(response) ===
{response}

-> END
