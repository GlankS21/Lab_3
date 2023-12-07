using Lab_3.Model;
using Lab_3.Musics;


namespace Lab_3.Core;

public class MusicRepository :IMusicRepository{
    private readonly MusicCatalogContext _dbContext;
    public MusicRepository(MusicCatalogContext dbContext) {
        _dbContext = new MusicCatalogContext();
        _dbContext.Database.EnsureCreated(); // true если база данных создана, false если она уже существует.
    }
    
    public List<MusicModel> GetAll() {
        if (_dbContext.Musics == null) return new List<MusicModel>();
        return _dbContext.Musics.ToList();
    }
    
    // list
    public void SetMusic(MusicModel music) {
        _dbContext.Musics.Add(music);
        _dbContext.SaveChanges();
    }
    
    //searchByAuthor
    public List<Music> FindByPartOfName(string PartOfName) {
        var musics = GetAll().Select(MusicModel => {
            return new Music(MusicModel.author, MusicModel.composition);
        }).ToList();
        List<Music> resultMusic = new List<Music>();
        
        foreach (var _music in musics) {
            if(_music.compositionName.Contains(PartOfName))
                resultMusic.Add(_music);
        }
        
        return resultMusic;
    }
    //delete
    public void DeleteMusic(string title) {
        _dbContext.Remove(FindByTitle(title));
        _dbContext.SaveChanges();
    }
    
    private MusicModel FindByTitle(string title) {
        var elements = title.Split(" - ");
        return _dbContext.Musics.SingleOrDefault(music => music.author == elements[0] && music.composition == elements[1]);
    }
}