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
        -> goingParty("Awesome! It's going to be a blast.")
    + [No, I can't make it.]
        -> notGoingParty("Oh, that's a shame. Maybe next time.")

=== helloMarcus(response) ===
{response}
    + [What's up?]
        -> whatsUp("Just checking in. Wanted to see if you got my message.")
    + [Not much.]
        -> notMuch("Alright, talk to you later.")

=== goingParty(response) ===
{response}
    + [What time does it start?]
        -> partyTime("It starts around 8 PM. Don't be late!")
    + [Should I bring anything?]
        -> bringSomething("If you can, bring some snacks or drinks. Thanks!")

=== notGoingParty(response) ===
{response}
    + [Maybe next time, then.]
        -> last("Yeah, we’ll catch up soon.")
    + [Want to hang out another day instead?]
        -> hangOut("Sure! How about tomorrow afternoon?")

=== whatsUp(response) ===
{response}
    + [Did you get my message?]
        -> messageReceived("Yes! I got it, thanks for checking.")
    + [Just calling to chat.]
        -> justChat("Nice! It's always good to hear from you.")

=== notMuch(response) ===
{response}
    + [Want to grab a coffee later?]
        -> coffee("Sounds good! Where should we meet?")
    + [Talk to you later, then.]
        -> last("Alright, catch you later!")

=== partyTime(response) ===
{response}
    + [See you there!]
        -> last("Can't wait! See you tonight.")
    + [Might be a little late.]
        -> last("No worries, just come when you can.")

=== bringSomething(response) ===
{response}
    + [Got it, see you!]
        -> last("Thanks! See you tonight.")
    + [I might forget, sorry!]
        -> last("No problem! Just come and have fun.")

=== hangOut(response) ===
{response}
    + [Tomorrow works for me.]
        -> last("Great! Looking forward to it.")
    + [I might be busy, let's plan later.]
        -> last("Okay, we'll figure something out.")

=== messageReceived(response) ===
{response}
    + [Good to hear!]
        -> last("Yep, all good!")

=== justChat(response) ===
{response}
    + [Cool, how's everything?]
        -> last("Things are good, just keeping busy.")
    + [Nice talking to you!]
        -> last("You too! Talk soon.")

=== coffee(response) ===
{response}
    + [Let's meet at the cafe downtown.]
        -> last("Perfect! See you there.")
    + [Maybe another time?]
        -> last("Alright, we’ll plan later then.")

=== last(response) ===
{response}

-> END
