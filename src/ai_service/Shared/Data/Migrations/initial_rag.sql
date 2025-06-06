CREATE TABLE Texts (
    Id UUID UNIQUE NOT NULL PRIMARY KEY,
    Content TEXT NOT NULL           
);

CREATE TABLE Tags (
    Id UUID UNIQUE NOT NULL PRIMARY KEY,
    Name VARCHAR(50) NOT NULL UNIQUE    
);

CREATE TABLE Vectors (
    Id UUID UNIQUE NOT NULL PRIMARY KEY,                
    TextId UUID NOT NULL,                  
    CreatedAt TIMESTAMPTZ DEFAULT NOW(),  
    CONSTRAINT fk_text_id FOREIGN KEY (TextId) REFERENCES Texts(Id) ON DELETE CASCADE 
);

CREATE TABLE VectorTags (
    VectorId UUID NOT NULL REFERENCES Vectors(Id) ON DELETE CASCADE, 
    TagId UUID NOT NULL REFERENCES Tags(Id) ON DELETE CASCADE,   
    PRIMARY KEY (VectorId, TagId)
);

CREATE INDEX idx_vectortags_vectorid ON VectorTags (VectorId);
