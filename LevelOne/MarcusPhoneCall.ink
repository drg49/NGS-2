-> start

=== start ===
Hey sorry, did I call too early?
    + [Hey Marcus, nope I just woke up.]
        -> justWoke("Figured. You always sleep in on your days off. Anyway, David and I were talking about grabbing lunch today.")
    + [Hello Marcus, I am a bit tired.]
        -> helloMarcus("Heard. I won't talk for long. Did you want to meet for lunch still with David and I.")

=== justWoke(response) ===
{response}
    + [Yeah, lunch sounds good.]
        -> goingLunch("Knew you'd be up for it. David's already hungry.")
    + [Honestly, I was planning to stay in.]
        -> hesitantLunch("Come on, you can't bail. We already counted you in.")

=== hesitantLunch(response) ===
{response}
    + [Alright, fine.]
        -> goingLunch("There we go. I knew you'd cave.")
    + [I really don't feel like going out.]
        -> noChoiceLunch("I get it, but you kinda don't have a choice this time.")

=== noChoiceLunch(response) ===
{response}
    + [What do you mean?]
        -> explainLunch("David already made the reservation. And he'll be pissed if you don't show.")
    + [Seriously?]
        -> explainLunch("Seriously. Just trust me on this.")

=== explainLunch(response) ===
{response}
    + [Alright, I'll come.]
        -> goingLunch("Good. You won't regret it.")
    + [You guys owe me.]
        -> goingLunch("Fair. Lunch is on David.")

=== helloMarcus(response) ===
{response}
    + [Yeah, I remember.]
        -> goingLunch("Good. Same place as usual - Lucca's Pizza Shop.")
    + [I was thinking about skipping.]
        -> hesitantLunch("Not today. We need you there.")

=== goingLunch(response) ===
{response}
    + [What time are we meeting?]
        -> lunchTime("About an hour. Gives you time to wake up.")
    + [Where again?]
        -> lunchTime("Lucca's Pizza Shop, the best in town.")

=== lunchTime(response) ===
{response}
    + [Alright, I'll head out soon.]
        -> final("Perfect. I'll see you at Lucca's.")
    + [Give me a minute to get ready.]
        -> final("Take your time. Just don't be late - we'll be at Lucca's.")

=== final(response) ===
{response}

-> END
