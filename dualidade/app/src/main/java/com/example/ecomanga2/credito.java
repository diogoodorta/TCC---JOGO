package com.example.ecomanga2;

import androidx.appcompat.app.AppCompatActivity;

import android.content.Context;
import android.content.Intent;
import android.net.Uri;
import android.os.Bundle;
import android.os.Vibrator;
import android.view.View;
import android.widget.Button;


public class credito extends AppCompatActivity {

    @Override
    protected void onCreate(Bundle savedInstanceState) {
        super.onCreate(savedInstanceState);
        setContentView(R.layout.activity_credito);

    }

    private void vibrar() {

        Vibrator rr = (Vibrator) getSystemService(Context.VIBRATOR_SERVICE);
        long milisegundos = 100;
        rr.vibrate(milisegundos);
    }

    public void irParaSitePerfilCaio(View view) {
        Uri uri = Uri.parse("https://github.com/caioolsilva");
        Intent it = new Intent(Intent.ACTION_VIEW, uri);
        startActivity(it);

        vibrar();
    }

    public void irParaSitePerfilDiogo(View view) {
        Uri uri = Uri.parse("https://github.com/diogoodorta");
        Intent it = new Intent(Intent.ACTION_VIEW, uri);
        startActivity(it);

        vibrar();
    }

    public void irParaSitePerfilGustavo(View view) {
        Uri uri = Uri.parse("https://github.com/GSPMSilva");
        Intent it = new Intent(Intent.ACTION_VIEW, uri);
        startActivity(it);

        vibrar();
    }

    public void irParaSitePerfilKaue(View view) {
        Uri uri = Uri.parse("https://github.com/kajogoss");
        Intent it = new Intent(Intent.ACTION_VIEW, uri);
        startActivity(it);

        vibrar();
    }

    public void irParaSitePerfilJoãoC(View view) {
        Uri uri = Uri.parse("https://github.com/Joaopc22");
        Intent it = new Intent(Intent.ACTION_VIEW, uri);
        startActivity(it);

        vibrar();
    }

    public void irParaSitePerfilJoãoA(View view) {
        Uri uri = Uri.parse("https://github.com/joao-alfieri");
        Intent it = new Intent(Intent.ACTION_VIEW, uri);
        startActivity(it);

        vibrar();
    }

    public void irParaSiteProjeto(View view) {
        Uri uri = Uri.parse("https://github.com/diogoodorta/TCC---JOGO");
        Intent it = new Intent(Intent.ACTION_VIEW, uri);
        startActivity(it);

        vibrar();
    }

}