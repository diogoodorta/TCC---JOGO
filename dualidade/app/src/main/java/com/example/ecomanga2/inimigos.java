package com.example.ecomanga2;

import androidx.appcompat.app.AppCompatActivity;

import android.os.Bundle;
import android.content.Intent;
import android.view.View;

public class inimigos extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_inimigos);
    }

    public void IrParaTelaMenu(View view) {
        Intent novaTela = new Intent(inimigos.this, MainActivity.class);
        startActivity(novaTela);
    }
}