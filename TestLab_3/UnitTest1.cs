using AutoFixture;
using Lab_3.Core;
using Lab_3.Model;
using Lab_3.Musics;
using NSubstitute;

namespace TestLab_3;

public class UnitTest1
{
    private Fixture _fixture = new Fixture();
    [Fact]
    public void FunctionSearch_ReturnsExpected()
    {
        var musics = _fixture.Build<MusicModel>()
            .With(x => x.author, "author")
            .With(x => x.composition, "composition").CreateMany();

        var repo = Substitute.For<IMusicRepository>();
        repo.GetAll().Returns(musics);

        var musicService = new MusicCatalog(repo);
        var result = musicService.listMusic();

        Assert.NotNull(result);
    }
}