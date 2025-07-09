using UnityEngine;
using System.Collections.Generic;
using System.Linq;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NamingConventions;
using System.IO;

public class DialogueManager: MonoBehaviour
{
    public static DialogueManager Instance;
    public int currentDay = 1;
    private Dictionary<int, List<DialogueScene>> dialogueByDay = new();

    void Awake()
    {
        if(Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
        
        LoadDialogue();
    }

    private void LoadDialogue()
    {
        TextAsset yamlFile = Resources.Load<TextAsset>("Dialogue/dialogue");

        if(yamlFile == null)
        {
            Debug.LogError("Dialogue YAML file not found!");
            // DevConsole.ErrorAlert("Dialogue YAML file not found!"); // Uncomment if a developer console is available, for my case later on I will use a source engine like developer console.
            return;
        }

        var input = new StringReader(yamlFile.text);
        var deserializer = new DeserializerBuilder()
            .WithNamingConvention(CamelCaseNamingConvention.Instance)
            .Build();

        DialogueRoot dialogueRoot = deserializer.Deserialize<DialogueRoot>(input);
        foreach(var day in dialogueRoot.days)
        {
            dialogueByDay[day.day] = day.scenes;
        }
    }

    public DialogueScene GetRandomScene(DialogueType type)
    {
        if (!dialogueByDay.TryGetValue(currentDay, out var scenes))
            return null;
        
        var filtered = scenes.Where(s => s.type == type).ToList();
        if (filtered.Count == 0) return null;

        return filtered[Random.Range(0, filtered.Count)];
    }

    public List<DialogueLine> GetRandomVariantLines(DialogueScene scene)
    {
        if (scene.variants == null || scene.variants.Count == 0) return null;
        var variant = scene.variants[Random.Range(0, scene.variants.Count)];
        return variant.lines;
    }

    public void StartDialogue(DialogueType type)
    {
        var scene = GetRandomScene(type);
        if (scene == null)
        {
            Debug.LogWarning("No scene found for type " + type);
            return;
        }

        var lines = GetRandomVariantLines(scene);
        StartCoroutine(RunDialogue(lines));
    }

    private IEnumerator<WaitForSeconds> RunDialogue(List<DialogueLine> lines)
    {
        foreach (var line in lines)
        {
            Debug.Log($"{line.speaker}: {line.text}");
            // Hook into UI subtitle system here
            yield return new WaitForSeconds(2f);
        }
    }
}