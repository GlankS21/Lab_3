using Lab_3.Core;
using Lab_3.Model;

namespace Lab_3.Musics;

public class MusicCatalog:IMusicCatalog
{
    private IMusicRepository _musics;
    public MusicCatalog(IMusicRepository musicRepository) {
        _musics = musicRepository;
    }
    
    
    public List<Music> listMusic() {
        Console.WriteLine("All compositions in catalog:");
        List<Music> musics = _musics.GetAll().Select(MusicModel => {
            return new Music(MusicModel.author, MusicModel.composition);
        }).ToList();
        
        foreach (var _music in musics) {
            Console.WriteLine(_music.getMusic());   
        }
        
        return musics;
    }

    public bool seachMusic(string PartOfName) {
        List<Music> resultMusic = _musics.FindByPartOfName(PartOfName);
        
        if (resultMusic.Count == 0) {
            Console.WriteLine("No one item was found by this criteria.");
            return false;
        }
        Console.WriteLine("Results found:");
        foreach (var _music in resultMusic)
            Console.WriteLine(_music.getMusic());
            return true;
    }

    public void addMusic(Music music) {
        var newMusicModel = new MusicModel() {
            Id = music.Id,
            author = music.authorName,
            composition = music.compositionName
        };
        _musics.SetMusic(newMusicModel);
    }

    public void deleteMusic(string name) {
        List<Music> musics = _musics.GetAll().Select(MusicModel => {
            return new Music(MusicModel.author, MusicModel.composition);
        }).ToList();
        
        var find = false;
        for (var i = 0; i < musics.Count; i++) {
            if (musics[i].getMusic() == name) {
                Console.WriteLine($"Track '{name}' deleted.");
                _musics.DeleteMusic(name);
                find = true;
                break;
            }
        }
        if(!find) Console.WriteLine("Music not found");
    }
}