# Mock api pour grics
- adresse UI https://sgh-api.web.app/
- adresse Serveur https://simpleapi20210425111657.azurewebsites.net/api/queries
## Route Api
#### Authorization : 
- POST : `/api/authentication`
```
// application/json
{
  "email":"manager@grics.ca",
  "password":"password123"
}
```
#### Génération des données : La fonctionnalité des routes a été désactivée
- POST : `/api/mockgenerator/generate_schools`
- POST : `/api/mockgenerator/generate_teachers`
- POST : `/api/mockgenerator/generate_students`

#### Recherche des données : une seule route avec des paramètres passés en corps
- POST : `/api/Queries`
```
// Exemple requête de tous les écoles
{   
    "Target":"School"
}
// Exemple requête de tous les écoles, prénom : loic et sexe : masculin
{   
    "Target":"Student",
    "Where":{
        "FirstName":"loic",
        "Sex" :"M"
    }
}
// Exemple requête des étudiants avec un prénom : loic qui vont à l'école outremont
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
// Exemple requête des étudiants avec un prénom : loic, nom de famille : leclerc, qui vont à l'école outremont
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
// Exemple bulletin de note pour loic leclerc qui va à l'école outremont
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
Les données liées à ce projet ont été générées de façons aléatoire, elles se divisent en quatre classes qui représentent les écoles(School), les enseignants(Teacher), les élèves(Student) et leur bulletin de notes (ReportCard).
Les données sont sauvegardées dans une base données NoSql, Google Firestore.
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
L'authentification se fait avec les json web token (JWT) en tant que Bearer token dans le _Authorization header_.
