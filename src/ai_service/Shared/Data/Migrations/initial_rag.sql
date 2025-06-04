CREATE TABLE Texts (
    Id SERIAL PRIMARY KEY,
    Content TEXT NOT NULL           
);

CREATE TABLE Tags (
    Id SERIAL PRIMARY KEY,
    Name VARCHAR(50) NOT NULL UNIQUE    
);

CREATE TABLE Vectors (
    Id SERIAL PRIMARY KEY,                
    TextId INT NOT NULL,                  
    VectorId UUID UNIQUE NOT NULL DEFAULT gen_random_uuid(),  
    CreatedAt TIMESTAMPTZ DEFAULT NOW(),  
    CONSTRAINT fk_text_id FOREIGN KEY (TextId) REFERENCES Texts(Id) ON DELETE CASCADE 
);

CREATE TABLE VectorTags (
    VectorId UUID NOT NULL REFERENCES Vectors(VectorId) ON DELETE CASCADE, 
    TagId INT NOT NULL REFERENCES Tags(Id) ON DELETE CASCADE,   
    PRIMARY KEY (VectorId, TagId)
);

CREATE INDEX idx_vectortags_vectorid ON VectorTags (VectorId);