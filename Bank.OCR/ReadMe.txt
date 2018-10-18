Here we have a my simple implementation of the requirements highlighted in the coding dojo bank ocr.  I have opted for this approach
in an effort to adhere to the solid principles as much as possible - hence I felt that there are effectively 3 different processes
taking place; File Reading and Writing, Reading and handling of accountentries as well as their conversion into account numbers, and
validation.

Based on the spec and the assumption that all inputs are correct, there is limited error handling.

In an attempt to keep the footprint of the application as small as possible and I have attempted to avoid using things like Linq 
or Generic Collections (these are used in the tests but I think that's OK) and pre-setting the size of arrays at instantiation.

I've used interfaces for abstraction and constructor injection, and make use of those in my tests for mocking.

Made an attempt to implement some sort of file configuration so that the 'basepath' can be changed at composition.  Had thought about
adding .Net Core configuration (Extensions.Configuration.Json) but decided against it - felt it was over kill.

Opted for composition instead of DI as the application is very small and doesn't really warrant one for such a simple object graph.  Also 
very easy to read.

opted to use some static and constants for things that wont change and to keep things DRY.  Also reduces the number of places
that would need to be updated should things like the MaximumAccountEntries or RowsPerAccountEntry change.

For testing tried to use TDD as much as possible...as such I've only tried to write tests around the requirements and no too much else.  In 
addition to that I've opted to use XUnit mainly as that's what I'm used to.  In an attempt to aid the readability of the test I've
also used BDDFY nuget package to take advantage of the Gherkin syntax (Given, When, Then).  Have also opted to use AutoMocker for
mocking, etc as a preference.

The solution layout is based on how I would configure a DDD based project...left out the Domain folder as no need.  These would usually
be broken out into seperate projects.  Opted to use an Application Layer to pull everything together and instead of using the Program
class giving the option to move to a different UI.

Have generally kept the interfaces and classes in the same file as a preference (as suggested by Vaughn Vernon in "Implementing DDD").

Used async/await for file access as it's potentially blocking.

Could also add an integration tests so we can see it works correctly without the need to run and test manually.