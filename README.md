# Mock api pour grics
adresse UI https://sgh-api.web.app/
adresse Serveur https://simpleapi20210425111657.azurewebsites.net/api/queries
## Route Api
#### Authorization : 
POST : `/api/authentication`
```
// application/json
{
  "email":"manager@grics.ca",
  "password":"password123"
}
```
#### Génération des données : La fonctionnalité des routes a été désactivé
POST : `/api/mockgenerator/generate_schools`
POST : `/api/mockgenerator/generate_teachers`
POST : `/api/mockgenerator/generate_students`

#### Recherche des données : une seule route avec des paramètres passés en corps
POST : `/api/Queries`
```
// Exemple requete de tous les ecoles
{   
    "Target":"School"
}
// Exemple requete de tous les ecoles, prenom loic et sexe masculin
{   
    "Target":"Student",
    "Where":{
        "FirstName":"loic",
        "Sex" :"M"
    }
}
// Exemple requete des etudiants avec un prenom loic qui vont a lecole outremont
{   
    "Target":"Student",
    "Where":{
        "FirstName":"loic"
    },
    "Compound":{
        "School":
        {
            "Name":"outremont"
        }
    }
}
// Exemple requete des etudiants avec un prenom loic, nom famille leclerc, qui vont a lecole outremont
{   
    "Target":"Student",
    "Where":{
        "FirstName":"loic",
        "LastName":"leclerc"
    },
    "Compound":{
        "School":
        {
            "Name":"outremont"
        }
    }
}
// Exemple bulletin de note pour loic leclerc qui va a l'ecole outremont
{   
    "Target":"ReportCard",
    "Compound":{
        "Student":{
            "FirstName":"loic",
            "LastName":"leclerc"
        },
        "School":
        {
            "Name":"outremont"
        }
    }
}
```
## Données
Les données liées à ce projet ont été généré de façons aléatoire, elles comptent quatre classes qui représentent les écoles(School), les enseignants(Teacher), les élèves(Student) et leur bulletin de notes (ReportCard).
Les données sont sauvegardé dans une base données NoSql soit google firestore.
### School
```
    Id: string,
    Name: string,
    TelephoneNumber: string
```
### Teacher
```
    Id: string,
    FirstName: string,
    LastName: string,
    Sex: string
    SchoolId: string
```
### Student
```
    Id: string,
    FirstName: string,
    LastName: string,
    Sex: string,
    Dob: DateTime,
    TeacherId: string,
    SchoolId: string
```
### ReportCard
```
    Id: string,
    TeacherId: string,
    SchoolId: string,
    StudentId: string
    ...
```

## Authorization/Authentification
L'authentification se fait via JWT json web token en tant que Bearer token dans le authorization header.
