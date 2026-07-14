# KI-Nutzungsprotokoll zur Studienarbeit

## Angaben zur Arbeit

- Gruppenname: StudyMate
- Titel der Studienarbeit: StudyMate - AI-powered Study Assistant
- KI genutzt: ja
- Verwendete KI-Werkzeuge: LLM Chat Assistant

Wenn keine KI genutzt wurde, reicht hier die Angabe "nein". In diesem Fall müssen die folgenden Abschnitte nicht ausgefüllt werden.

## Kurz-Erklärung

Dieses Protokoll wird als Markdown-Datei im Git-Repository der Gruppe geführt.
Wesentliche KI-Nutzungen werden hier kurz und zeitnah dokumentiert.
Die Nachvollziehbarkeit über Versionen ergibt sich aus der Git-Historie dieser Datei.

Für diese Studienarbeit wurden KI-Werkzeuge als Unterstützung verwendet.
Die wesentlichen Nutzungen sind unten dokumentiert.
Alle übernommenen Inhalte wurden fachlich geprüft, bei Bedarf angepasst und in die Arbeit eigenverantwortlich integriert.

## Übersicht der KI-Nutzung

Tragen Sie hier die wesentlichen Nutzungen ein.
Wenn ähnliche Nutzungen in engem Zusammenhang stehen, können Sie sie zusammenfassen.
Pflegen Sie das Protokoll möglichst zeitnah, damit die Git-Historie die Entwicklung nachvollziehbar macht. Nutzen Sie KI, um Ihren Promtverlauf entsprechnd dieser Vorlage festzuhalten.  

| Datum | Anwender der KI | Werkzeug | Nutzung kurz beschrieben | Übernahme und Anpassung kurz beschrieben |
| --- | --- | --- | --- | --- |
| 14.07.2026 | Truc Trinh | Lisa Pro | Backend File Service Implementation | **AI Guidance:**<br>- Explained Repository vs Service layer architecture<br>- Explained FileStorageService vs StudyFileService separation of concerns<br>- Identified bug in UpdateFileAsync: wrong delete logic (called repo.DeleteAsync instead of storage.DeleteFileAsync)<br>- Identified bug in FileStorageService: invalid path with @-character<br>- Reviewed and validated all code implementations<br><br>**My proactive work:**<br>- Created documentation folder structure<br>- Implemented all repository classes (IStudyFileRepository, StudyFileRepository)<br>- Implemented all service classes (IStudyFileService, StudyFileService, FileStorageService)<br>- Fixed all bugs identified by AI<br>- Reviewed and tested all code before commit<br>- Made all final decisions on architecture and implementation |
|  |  |  |  |  |
|  |  |  |  |  |
|  |  |  |  |  |

## Optionale ergänzende Hinweise

Hier können Sie bei Bedarf kurz ergänzen,

- wie Sie mit fehlerhaften KI-Antworten umgegangen sind,
- welche Vorschläge Sie bewusst verworfen haben,
- in welchen Fällen die KI nur als Sparringspartner diente.

## Eigenständigkeit und Verantwortung

Wir bestätigen, dass die KI-Nutzung in dieser Arbeit vollständig und nach bestem Wissen dokumentiert wurde.
Wir übernehmen die Verantwortung für die fachliche Richtigkeit, die Auswahl der übernommenen Inhalte und die gesamte abgegebene Arbeit.

- Datum: 14.07.2026
- Gruppenname: StudyMate